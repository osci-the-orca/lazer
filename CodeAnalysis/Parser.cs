
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Lazer.CodeAnalysis;


internal sealed class Parser
{
    private readonly SyntaxToken[] _tokens;
    private int _position;
    private readonly List<string> _diagnostics = new();
    public IEnumerable<string> Diagnostics => _diagnostics;
    public Parser(string text)
    {
        var tokens = new List<SyntaxToken>();

        var lexer = new Lexer(text);

        SyntaxToken token;

        do
        {
            token = lexer.Lex();
            if (token.Kind != SyntaxKind.WhiteSpaceToken && token.Kind != SyntaxKind.BadToken)
            {
                tokens.Add(token);
            }

        } while (token.Kind != SyntaxKind.EndOfFileToken);

        _tokens = tokens.ToArray();
        _diagnostics.AddRange(lexer.Diagnostics);
    }

    private SyntaxToken Peek(int offset)
    {
        var index = _position + offset;
        if (index >= _tokens.Length - 1)
        {
            return _tokens[^1];
        }

        return _tokens[index];
    }

    private SyntaxToken Current => Peek(0);

    private SyntaxToken NextToken()
    {
        var current = Current;
        _position++;
        return current;
    }

    private SyntaxToken MatchToken(SyntaxKind kind)
    {
        if (Current.Kind == kind)
        {
            return NextToken();
        }

        _diagnostics.Add($"Error: Bad token <{Current.Kind}>, expected: {kind}");

        return new SyntaxToken(kind, Current.Position, null, null);
    }
    public SyntaxTree Parse()
    {
        var expression = ParseTerm();
        var endOfFileToken = MatchToken(SyntaxKind.EndOfFileToken);
        return new SyntaxTree(_diagnostics, expression, endOfFileToken);
    }
    private ExpressionSyntax ParseExpression()
    {
        return ParseTerm();
    }

    private ExpressionSyntax ParseTerm()
    {
        var left = ParseFactor();

        while (Current.Kind == SyntaxKind.PlusToken || Current.Kind == SyntaxKind.MinusToken)
        {
            var operatorToken = NextToken();
            var right = ParseFactor();
            left = new BinaryExpressionSyntax(left, operatorToken, right);
        }
        return left;
    }

    private ExpressionSyntax ParseFactor()
    {
        var left = ParsePrimaryExpression();

        while (Current.Kind == SyntaxKind.AstericsToken ||
                Current.Kind == SyntaxKind.ForwardSlashToken)
        {
            var operatorToken = NextToken();
            var right = ParsePrimaryExpression();
            left = new BinaryExpressionSyntax(left, operatorToken, right);
        }
        return left;
    }

    private ExpressionSyntax ParsePrimaryExpression()
    {
        if (Current.Kind == SyntaxKind.OpenParenthesisToken)
        {
            var left = NextToken();
            var expression = ParseExpression();
            var right = MatchToken(SyntaxKind.CloseParenthesisToken);
            return new ParenthesizedExpressionSyntax(left, expression, right);
        }

        var numberToken = MatchToken(SyntaxKind.NumberToken);
        return new LiteralExpressionSyntax(numberToken);
    }
}
