using System.ComponentModel;
using Lazer;

namespace Lazer.CodeAnalysis;
public sealed class Evaluator
{
    private readonly ExpressionSyntax _root;
    public Evaluator(ExpressionSyntax root)
    {
        _root = root;
    }

    public int Evaluate()
    {
        return EvaluateExpression(_root);
    }

    private int EvaluateExpression(ExpressionSyntax node)
    {
        if (node is NumberExpressionSyntax n && n.NumberToken.Value is not null)
        {
            return (int)n.NumberToken.Value;
        }
        if (node is BinaryExpressionSyntax b)
        {
            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);

            if (b.OperatorToken.Kind == SyntaxKind.PlusToken)
            {
                return left + right;
            }
            else if (b.OperatorToken.Kind == SyntaxKind.MinusToken)
            {
                return left - right;
            }
            else if (b.OperatorToken.Kind == SyntaxKind.AstericsToken)
            {
                return left * right;
            }
            else if (b.OperatorToken.Kind == SyntaxKind.ForwardSlashToken)
            {
                return left / right;
            }
            else
                throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}.");
        }

        if (node is ParenthesizedExpressionSyntax p)
        {
            return EvaluateExpression(p.Expression);
        }

        throw new Exception($"Unexepected node {node.Kind}");
    }
}