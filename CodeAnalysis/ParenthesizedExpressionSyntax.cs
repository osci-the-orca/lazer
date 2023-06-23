using Lazer.CodeAnalysis;

public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
{
    public SyntaxToken OpenParenthesisToken { get; }
    public ExpressionSyntax Expression { get; }
    public SyntaxToken ClosedParenthesisToken { get; }
    public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;

    public ParenthesizedExpressionSyntax(SyntaxToken openParenthesisToken, ExpressionSyntax expression, SyntaxToken closedParenthesisToken)
    {
        OpenParenthesisToken = openParenthesisToken;
        Expression = expression;
        ClosedParenthesisToken = closedParenthesisToken;
    }
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return OpenParenthesisToken;
        yield return Expression;
        yield return ClosedParenthesisToken;
    }
}