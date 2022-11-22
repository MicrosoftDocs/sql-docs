---
description: "Create and update statistics"
title: Create and update statistics
ms.service: sql
ms.subservice: 
ms.topic: reference
helpviewer_keywords:
  - "statistical information [SMO]"
ms.assetid: 47a0a172-a969-4deb-bca9-dd04401a0fe1
author: markingmyname
ms.author: maghan
ms.reviewer: matteot
ms.custom: seo-dt-2019
ms.date: 06/04/2020
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Create and update statistics

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

In SMO, statistical information about processing queries in the database can be collected by using the <xref:Microsoft.SqlServer.Management.Smo.Statistic> object.

It's possible to create statistics for any column by using the <xref:Microsoft.SqlServer.Management.Smo.Statistic> and <xref:Microsoft.SqlServer.Management.Smo.StatisticColumn> object. The <xref:Microsoft.SqlServer.Management.Smo.Statistic.Update%2A> method can be run to update the statistics in the <xref:Microsoft.SqlServer.Management.Smo.Statistic> object. The results can be viewed in the Query Optimizer.

## Example

To use any code example that is provided, you can choose the programming environment, the programming template, and the programming language in which to create your application. For more information, see [Create a Visual C&#35; SMO Project in Visual Studio .NET](../../../relational-databases/server-management-objects-smo/how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).

## Create and update statistics in Visual Basic

This code example creates a new table on an existing database for which the <xref:Microsoft.SqlServer.Management.Smo.Statistic> object and the <xref:Microsoft.SqlServer.Management.Smo.StatisticColumn> object are created.

```vb
'Connect to the local, default instance of SQL Server.
Dim srv As Server
srv = New Server
'Reference the AdventureWorks2012 database.
Dim db As Database
db = srv.Databases("AdventureWorks2012")
'Reference the CreditCard table.
Dim tb As Table
tb = db.Tables("CreditCard", "Sales")
'Define a Statistic object by supplying the parent table and name arguments in the constructor.
Dim stat As Statistic
stat = New Statistic(tb, "Test_Statistics")
'Define a StatisticColumn object variable for the CardType column and add to the Statistic object variable.
Dim statcol As StatisticColumn
statcol = New StatisticColumn(stat, "CardType")
stat.StatisticColumns.Add(statcol)
'Create the statistic counter on the instance of SQL Server.
stat.Create()
```

## Create and update statistics in C#

This code example creates a new table on an existing database for which the <xref:Microsoft.SqlServer.Management.Smo.Statistic> object and the <xref:Microsoft.SqlServer.Management.Smo.StatisticColumn> object are created.

```csharp
public static void CreatingAndUpdatingStatistics()
{
    // Connect to the local, default instance of SQL Server.
    var srv = new Server();

    // Reference the AdventureWorks2012 database.
    var db = srv.Databases["AdventureWorks"];

    // Reference the CreditCard table.
    var tb = db.Tables["CreditCard", "Sales"];

    // Define a Statistic object by supplying the parent table and name
    // arguments in the constructor.
    var stat = new Statistic(tb, "Test_Statistics");

    // Define a StatisticColumn object variable for the CardType column
    // and add to the Statistic object variable.
    var statcol = new StatisticColumn(stat, "CardType");
    stat.StatisticColumns.Add(statcol);

    //Create the statistic counter on the instance of SQL Server.
    stat.Create();

    // List all the statistics object on the table (you will see the newly created one)
    foreach (var s in tb.Statistics.Cast<Statistic>())
        Console.WriteLine($"{s.ID}\t{s.Name}");

    // Output:
    //  2       AK_CreditCard_CardNumber
    //  1       PK_CreditCard_CreditCardID
    //  3       Test_Statistics
 }
```

## Create and update statistics in PowerShell

This code example creates a new table on an existing database for which the <xref:Microsoft.SqlServer.Management.Smo.Statistic> object and the <xref:Microsoft.SqlServer.Management.Smo.StatisticColumn> object are created.

```powershell
Import-Module SQLServer

# Connect to the local, default instance of SQL Server.  
$srv = Get-Item SQLSERVER:\SQL\localhost\DEFAULT

# Reference the AdventureWorks database.
$db = $srv.Databases["AdventureWorks"]

# Reference the CreditCard table.
$tb = $db.Tables["CreditCard", "Sales"]

# Define a Statistic object by supplying the parent table and name
# arguments in the constructor.
$stat = New-Object Microsoft.SqlServer.Management.Smo.Statistic($tb, "Test_Statistics")

# Define a StatisticColumn object variable for the CardType column
# and add to the Statistic object variable.
$statcol = New-Object Microsoft.SqlServer.Management.Smo.StatisticColumn($stat, "CardType")
$stat.StatisticColumns.Add($statcol)

# Create the statistic counter on the instance of SQL Server.
$stat.Create()

# Finally dump all the statistics (you can see the newly created one at the bottom)
$tb.Statistics

# Output:
# Name                                Last Updated Is From Index  Statistic Columns
#                                                  Creation
# ----                                ------------ -------------- -----------------
# AK_CreditCard_CardNumber      10/27/2017 2:33 PM True           {CardNumber}
# PK_CreditCard_CreditCardID    10/27/2017 2:33 PM True           {CreditCardID}
# Test_Statistics                 6/4/2020 8:11 PM False          {CardType}
```
