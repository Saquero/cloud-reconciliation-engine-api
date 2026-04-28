namespace CleanDddHexagonal.Domain.Enums;

public enum ReconciliationIssueType
{
    SeatCountMismatch = 1,
    CostMismatch = 2,
    MissingInternalRecord = 3,
    MissingExternalSnapshot = 4
}
