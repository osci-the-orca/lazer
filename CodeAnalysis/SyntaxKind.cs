namespace Lazer.CodeAnalysis;
public enum SyntaxKind
{
    NumberToken,
    WhiteSpaceToken,
    PlusToken,
    MinusToken,
    AstericsToken,
    ForwardSlashToken,
    OpenParenthesisToken,
    CloseParenthesisToken,
    BadToken,
    EndOfFileToken,
    NumberExpression,
    BinaryExpression,
    ParenthesizedExpression,
}