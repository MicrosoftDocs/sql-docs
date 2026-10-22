---
title: Modify data with stored procedures
description: Call stored procedures with SqlCommand, configure input and output parameters, process result sets, and read return values.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, paulmedynski, cmalhotra
ms.date: 10/13/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# Modify data with stored procedures

Set <xref:Microsoft.Data.SqlClient.SqlCommand.CommandType%2A> to `CommandType.StoredProcedure` and set `CommandText` to the schema-qualified procedure name. Add one parameter for each procedure parameter, with matching type, size, and direction.

The examples in this article assume that an open <xref:Microsoft.Data.SqlClient.SqlConnection> named `connection` targets the [AdventureWorks sample database](../../samples/adventureworks-install-configure.md).

The C# examples require `using Microsoft.Data.SqlClient;`. Examples that use `SqlDbType` or `CommandType` also require `using System.Data;`.

<a id="example"></a>

## Call a stored procedure

AdventureWorks includes `dbo.uspGetEmployeeManagers`, which accepts one input parameter and returns a result set:

```csharp
using var command = new SqlCommand(
    "dbo.uspGetEmployeeManagers",
    connection);
command.CommandType = CommandType.StoredProcedure;
command.Parameters.Add(
    "@BusinessEntityID",
    SqlDbType.Int).Value = 6;

using SqlDataReader reader = command.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine(
        $"{reader.GetString(2)} {reader.GetString(3)}");
}
```

Use the parameter names declared by the procedure. Schema qualification avoids name-resolution ambiguity and unnecessary procedure-plan variants.

## Run a data-modification procedure

AdventureWorks also includes procedures that modify data. `HumanResources.uspUpdateEmployeePersonalInfo` replaces all of an employee's personal fields. The following example runs these steps inside a transaction:

1. Reads the employee's current values.
1. Changes the marital status.
1. Reads the change back.
1. Rolls back the transaction so the sample database is unchanged.

```csharp
const int businessEntityId = 1;
using SqlTransaction transaction = connection.BeginTransaction();

string nationalIdNumber;
DateTime birthDate;
string maritalStatus;
string gender;

using (var select = new SqlCommand(
    """
    SELECT NationalIDNumber, BirthDate, MaritalStatus, Gender
    FROM HumanResources.Employee
    WHERE BusinessEntityID = @BusinessEntityID;
    """,
    connection,
    transaction))
{
    select.Parameters.Add(
        "@BusinessEntityID",
        SqlDbType.Int).Value = businessEntityId;

    using SqlDataReader reader = select.ExecuteReader();
    if (!reader.Read())
    {
        throw new InvalidOperationException(
            $"Employee {businessEntityId} wasn't found.");
    }

    nationalIdNumber = reader.GetString(0);
    birthDate = reader.GetDateTime(1);
    maritalStatus = reader.GetString(2);
    gender = reader.GetString(3);
}

string newMaritalStatus = maritalStatus == "M" ? "S" : "M";

using (var command = new SqlCommand(
    "HumanResources.uspUpdateEmployeePersonalInfo",
    connection,
    transaction))
{
    command.CommandType = CommandType.StoredProcedure;
    command.Parameters.Add(
        "@BusinessEntityID",
        SqlDbType.Int).Value = businessEntityId;
    command.Parameters.Add(
        "@NationalIDNumber",
        SqlDbType.NVarChar,
        15).Value = nationalIdNumber;
    command.Parameters.Add(
        "@BirthDate",
        SqlDbType.DateTime).Value = birthDate;
    command.Parameters.Add(
        "@MaritalStatus",
        SqlDbType.NChar,
        1).Value = newMaritalStatus;
    command.Parameters.Add(
        "@Gender",
        SqlDbType.NChar,
        1).Value = gender;

    command.ExecuteNonQuery();
}

using (var verify = new SqlCommand(
    """
    SELECT MaritalStatus
    FROM HumanResources.Employee
    WHERE BusinessEntityID = @BusinessEntityID;
    """,
    connection,
    transaction))
{
    verify.Parameters.Add(
        "@BusinessEntityID",
        SqlDbType.Int).Value = businessEntityId;
    Console.WriteLine(
        $"Marital status: {maritalStatus} -> {verify.ExecuteScalar()}");
}

transaction.Rollback();
```

The procedure sets `SET NOCOUNT ON`, so `ExecuteNonQuery` returns `-1` instead of a row count. Don't use the return value to confirm the update. Read the row back instead.

Verify a procedure's contract and side effects before you execute it. Every command that runs on a connection with a pending local transaction must set `Transaction`. Ensure the procedure's transaction handling supports the outer transaction. This procedure catches errors and logs them with `dbo.uspLogError` instead of raising them to the caller.

## Read result sets

Use `ExecuteReader` or `ExecuteReaderAsync` when a procedure returns rows. When it returns more than one result set, call `NextResult` or `NextResultAsync` until it returns `false`.

Dispose the reader before reading output, input/output, or return-value parameters. Those values aren't final while the reader is open.

## Configure parameter directions

Set <xref:Microsoft.Data.SqlClient.SqlParameter.Direction%2A> for every parameter that isn't input-only:

```csharp
SqlParameter output = command.Parameters.Add(
    "@outputValue",
    SqlDbType.NVarChar,
    50);
output.Direction = ParameterDirection.Output;

SqlParameter inputOutput = command.Parameters.Add(
    "@counter",
    SqlDbType.Int);
inputOutput.Direction = ParameterDirection.InputOutput;
inputOutput.Value = 1;

SqlParameter returnValue = command.Parameters.Add(
    "@returnValue",
    SqlDbType.Int);
returnValue.Direction = ParameterDirection.ReturnValue;
```

Set `Size` for variable-length output and input/output parameters. An input/output parameter also needs an initial `Value`. A return-value parameter receives the integer from the procedure's Transact-SQL (T-SQL) `RETURN` statement. It doesn't receive a result set or a value selected by the procedure.

Read each parameter's `Value` only after execution finishes and any reader is closed:

```csharp
command.ExecuteNonQuery();

Console.WriteLine(output.Value);
Console.WriteLine(inputOutput.Value);
Console.WriteLine(returnValue.Value);
```

When a procedure doesn't assign an output parameter, its value can be `DBNull.Value`.

## Choose an execution method

Use the execution method that matches the procedure's result:

| Method | Use when |
| --- | --- |
| `ExecuteReader` | The procedure returns rows or multiple result sets. |
| `ExecuteNonQuery` | You need output parameters or a return value but don't need result rows. The client consumes any returned rows before the call completes. |
| `ExecuteScalar` | You need only the first column of the first row. |

`ExecuteNonQuery` returns affected-row counts when they're available. A procedure that uses `SET NOCOUNT ON` can modify rows and still return `-1`. Use an output parameter or return value when the procedure defines one as its success contract.

> [!NOTE]
> When a <xref:Microsoft.Data.SqlClient.SqlDataAdapter> uses a stored procedure to update or delete rows, don't use `SET NOCOUNT ON` if the adapter relies on the affected-row count for optimistic concurrency. A zero count can cause <xref:System.Data.DBConcurrencyException>.

## Handle failures

Catch <xref:Microsoft.Data.SqlClient.SqlException> around the execution call. A procedure can manage its own transaction and error handling, but the client must still decide whether the operation is safe to retry and whether its connection remains usable.

For server errors and `PRINT` messages, see [Handle errors and messages](handle-errors-messages.md).

## Related content

- [Configure parameters](configure-parameters.md)
- [Execute a command](execute-command.md)
- [Table-valued parameters](sql/table-valued-parameters.md)
- [Handle errors and messages](handle-errors-messages.md)
