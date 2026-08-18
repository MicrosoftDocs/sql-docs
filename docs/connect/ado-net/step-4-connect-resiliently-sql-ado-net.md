---
title: "Step 4: Connect Resiliently to SQL with ADO.NET"
description: Learn how to use retry logic to improve connection resiliency to a SQL database using ADO.NET.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, davidengel, paulmedynski, cmalhotra, randolphwest
ms.date: 08/14/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ms.custom:
  - sfi-ropc-nochange
dev_langs:
  - CSharp
ai-usage: ai-assisted
---
# Step 4: Connect resiliently to SQL with ADO.NET

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

- Previous article: [Step 3: Proof of concept connecting to SQL using ADO.NET](step-3-connect-sql-ado-net.md)

This article provides a C# code sample that demonstrates manually implemented retry logic. The retry logic handles temporary errors, or *transient faults*, that might clear if the program waits and tries the operation again.

> [!IMPORTANT]  
> For new applications that use Microsoft.Data.SqlClient 3.0 and later versions, prefer the driver's configurable retry logic instead of implementing a retry loop. Start with [Configure retry logic in SqlClient](configurable-retry-logic-sqlclient-introduction.md).

Sources of transient faults include:

- A brief failure of the network that supports the Internet.
- A cloud system might be load balancing its resources when your query arrives.

The ADO.NET classes for connecting to SQL Server can also connect to Azure SQL Database. Your client program can encounter transient faults. It should recover by using a bounded retry policy that is appropriate for the operation.

## Step 1: Identify transient errors

Your program must distinguish between transient errors and persistent errors. Transient errors are error conditions that might clear up in a short time, such as transient network problems. An example of a persistent error is a misspelling of the target database name. In that case, the "No such database found" error persists and doesn't clear up quickly.

The list of error numbers that are categorized as transient faults is available at [Error messages for SQL Database client applications](/azure/sql-database/sql-database-develop-error-messages/).

## Step 2: Create and run sample application

This sample assumes .NET Framework 4.6.2 or later is installed. The C# code sample consists of one file named `Program.cs`. Its code is provided in the next section.

### Step 2.a: Capture and compile the code sample

You can compile the sample with the following steps:

1. In the [free Visual Studio Community edition](https://visualstudio.microsoft.com/vs/community), create a new project from the C# Console Application template.

   - Select **File** > **New** > **Project** > **Installed** > **Templates** > **Visual C#** > **Windows** > **Classic Desktop** > **Console Application**.

   - Name the project `RetryAdo2`.

1. Open the Solution Explorer pane.

   - See the name of your project.
   - On your project, [add a NuGet dependency](/nuget/quickstart/install-and-use-a-package-in-visual-studio) on the Microsoft.Data.SqlClient package.
   - View the name of the `Program.cs` file.

1. Open the `Program.cs` file.

1. Replace the entire contents of the `Program.cs` file with the code in the following code block.

1. Select the menu **Build** > **Build Solution**.

### Step 2.b: Copy and paste sample code

Paste this code into your `Program.cs` file.

Then, edit the strings for the server name and password. You can find these strings in the method named `GetSqlConnectionString`.

> [!NOTE]  
> The connection string is geared toward Azure SQL Database, because it includes the four-character prefix `tcp:`. But you can adjust the server string to connect to your SQL Server instance.

```csharp
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading;

namespace RetryAdo2;

public class Program
{
    public static int Main(string[] args)
    {
        bool succeeded = false;
        const int totalNumberOfTimesToTry = 4;
        int retryIntervalSeconds    = 10;

        for (int tries = 1; tries <= totalNumberOfTimesToTry; tries++)
        {
            try
            {
                if (tries > 1)
                {
                    Console.WriteLine(
                        "Transient error encountered. Will begin attempt number {0} of {1} max...",
                        tries,
                        totalNumberOfTimesToTry
                    );
                    Thread.Sleep(1000 * retryIntervalSeconds);
                    retryIntervalSeconds = Convert.ToInt32(retryIntervalSeconds * 1.5);
                }
                AccessDatabase();
                succeeded = true;
                break;
            }
            catch (SqlException sqlExc) {
                if (TransientErrorNumbers.Contains(sqlExc.Number))
                {
                    Console.WriteLine("{0}: transient occurred.", sqlExc.Number);
                    continue;
                }

                Console.WriteLine(sqlExc);
                break;
            }
            catch (TestSqlException sqlExc) {
                if (TransientErrorNumbers.Contains(sqlExc.Number))
                {
                    Console.WriteLine("{0}: transient occurred. (TESTING.)", sqlExc.Number);
                    continue;
                }

                Console.WriteLine(sqlExc);
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                break;
            }
        }

        if (!succeeded) {
            Console.WriteLine("ERROR: Unable to access the database!");
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// Connects to the database, reads,
    /// prints results to the console.
    /// </summary>
    static void AccessDatabase() {
        //throw new TestSqlException(4060); //(7654321);  // Uncomment for testing.

        using var sqlConnection = new SqlConnection(GetSqlConnectionString());

        using var dbCommand = sqlConnection.CreateCommand();

        dbCommand.CommandText = @"
        SELECT TOP 3
            ob.name,
            CAST(ob.object_id as NVARCHAR(32)) AS [object_id]
        FROM sys.objects AS ob
        WHERE ob.type='IT'
        ORDER BY ob.name;";
        
        sqlConnection.Open();
        var dataReader = dbCommand.ExecuteReader();

        while (dataReader.Read())
        {
            Console.WriteLine(
                "{0}\t{1}",
                dataReader.GetString(0),
                dataReader.GetString(1)
            );
        }
    }

    /// <summary>
    /// Edit the four string values in accordance with your environment.
    /// </summary>
    /// <returns>An ADO.NET connection string.</returns>
    static private string GetSqlConnectionString()
    {
        // Prepare the connection string to Azure SQL Database.
        var sqlConnectionSB = new SqlConnectionStringBuilder
        {
            // Change these values to your values.
            DataSource           = "tcp:myazuresqldbserver.database.windows.net,1433", //["Server"]
            InitialCatalog       = "MyDatabase",                                       //["Database"]
            UserID               = "MyLogin",                                          // "@yourservername"  as suffix sometimes.
            Password             = "<password>",
            // Adjust these values if you like. (ADO.NET 4.5.1 or later.)
            ConnectRetryCount    = 3,
            ConnectRetryInterval = 10, // Seconds.
            // Leave these values as they are.
            IntegratedSecurity = false,
            Encrypt            = true,
            ConnectTimeout     = 30
        };

        return sqlConnectionSB.ToString();
    }

    static List<int> TransientErrorNumbers = new()
    {
        4060, 40197, 40501, 40613, 49918, 49919, 49920, 11001
    };
}

/// <summary>
/// For testing retry logic, you can have method
/// AccessDatabase start by throwing a new
/// TestSqlException with a Number that does
/// or does not match a transient error number
/// present in TransientErrorNumbers.
/// </summary>
internal class TestSqlException : ApplicationException
{
    internal TestSqlException(int testErrorNumber)
    {
        Number = testErrorNumber;
    }

    internal int Number { get; set; }
}
```

### Step 2.c: Run the program

The `RetryAdo2.exe` executable doesn't take parameters. To run the `.exe`:

1. Open a console window in the folder where you compiled the `RetryAdo2.exe` binary.
1. Run `RetryAdo2.exe` with no input parameters.

```output
database_firewall_rules_table   245575913
filestream_tombstone_2073058421 2073058421
filetable_updates_2105058535    2105058535
```

## Step 3: Ways to test your retry logic

You can simulate a transient error to test your retry logic in several ways.

### Step 3.a: Throw a test exception

The code sample includes:

- A small second class named `TestSqlException` with a property named `Number`.
- `//throw new TestSqlException(4060);` , which you can uncomment.

If you uncomment the throw statement and recompile, the next run of `RetryAdo2.exe` outputs something similar to the following.

```output
[C:\VS15\RetryAdo2\RetryAdo2\bin\Debug\]
>> RetryAdo2.exe
4060: transient occurred. (TESTING.)
Transient error encountered. Will begin attempt number 2 of 4 max...
4060: transient occurred. (TESTING.)
Transient error encountered. Will begin attempt number 3 of 4 max...
4060: transient occurred. (TESTING.)
Transient error encountered. Will begin attempt number 4 of 4 max...
4060: transient occurred. (TESTING.)
ERROR: Unable to access the database!

[C:\VS15\RetryAdo2\RetryAdo2\bin\Debug\]
>>
```

### Step 3.b: Retest with a persistent error

To prove the code handles persistent errors correctly, rerun the preceding test. Instead of using a real transient error number like 4060, use the fictional number 7654321. The program treats this number as a persistent error and bypasses any retry.

### Step 3.c: Disconnect from the network

1. Disconnect your client computer from the network.
   - For a desktop, unplug the network cable.
   - For a laptop, press the function combination of keys to turn off the network adapter.
1. Start `RetryAdo2.exe`, and wait for the console to display the first transient error, probably 11001.
1. Reconnect to the network, while `RetryAdo2.exe` continues to run.
1. Watch the console report success on a subsequent retry.

### Step 3.d: Temporarily misspell the server name

1. Temporarily add 40615 as another error number to `TransientErrorNumbers`, and recompile.
1. Set a breakpoint on the line: `new QC.SqlConnectionStringBuilder()`.
1. Use the *Edit and Continue* feature to purposely misspell the server name, a couple of lines below.
   - Let the program run and come back to your breakpoint.
   - The error 40615 occurs.
1. Fix the misspelling.
1. Let the program run and finish successfully.
1. Remove 40615, and recompile.

## Next step

> [!div class="nextstepaction"]
> [Application development overview - Azure SQL Database & Azure SQL Managed Instance](/azure/azure-sql/database/develop-overview)
