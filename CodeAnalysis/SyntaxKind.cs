namespace Lazer.CodeAnalysis;
public enum SyntaxKind
{
    //Tokens
    EndOfFileToken,
    BadToken,
    WhiteSpaceToken,
    NumberToken,
    PlusToken,
    MinusToken,
    AstericsToken,
    ForwardSlashToken,
    OpenParenthesisToken,
    CloseParenthesisToken,

    //Expressions
    LiteralExpression,
    BinaryExpression,
    ParenthesizedExpression,
}