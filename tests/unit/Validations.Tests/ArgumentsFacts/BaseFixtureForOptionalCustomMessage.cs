﻿namespace Triplex.Validations.Tests.ArgumentsFacts;

[TestFixture(false)]
[TestFixture(true)]
internal abstract class BaseFixtureForOptionalCustomMessage
{
    protected readonly bool UseCustomErrorMessage;

    protected BaseFixtureForOptionalCustomMessage(bool useCustomErrorMessage)
        => UseCustomErrorMessage = useCustomErrorMessage;
}
