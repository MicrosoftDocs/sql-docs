---
description: "MSSQLSERVER_6522"
title: MSSQLSERVER_6522
ms.custom: ""
ms.date: 12/25/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, vencher, tejasaks, docast
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "6522 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_6522
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|6522|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|SQLCLR_UDF_EXEC_FAILED|
|Message Text|A .NET Framework error occurred during execution of user defined routine or aggregate "%.*ls": %ls.|

## Explanation

Consider the following scenarios.

### Scenario 1

You create a common language runtime (CLR) routine that references a Microsoft .NET Framework assembly. The .NET Framework assembly is not documented in [922672](/troubleshoot/sql/database-design/policy-untested-net-framework-assemblies). Then, you install the .NET Framework 3.5 or a .NET Framework 2.0-based hotfix.

### Scenario 2

You create an assembly, and then you register the assembly in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Then, you install a different version of the assembly in the Global Assembly Cache (GAC).

When you execute the CLR routine or use the assembly from either of these scenarios in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you receive an error message that resembles the following:

> Server: Msg 6522, Level 16, State 2, Line 1  
A .NET Framework error occurred during execution of user defined routine or aggregate 'getsid':
>
> System.IO.FileLoadException: Could not load file or assembly 'System.DirectoryServices, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a' or one of its dependencies. Assembly in host store has a different signature than assembly in GAC. (Exception from HRESULT: 0x80131050)

## Possible cause

When the CLR loads an assembly, the CLR verifies that the same assembly is in the GAC. If the same assembly is in the GAC, the CLR verifies that the Module Version IDs (MVIDs) of these assemblies match. If the MVIDs of these assemblies do not match, you receive the error message that the [Explanation](#explanation) section mentions.

When an assembly is recompiled, the MVID of the assembly changes. Therefore, if you update the .NET Framework, the .NET Framework assemblies have different MVIDs because those assemblies are recompiled. Additionally, if you update your own assembly, the assembly is recompiled. Therefore, the assembly also has a different MVID.

## User action

### Action 1

To work around scenario 1 in the [Explanation](#explanation) section, you must manually update the .NET Framework assemblies in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To do this, use the `ALTER ASSEMBLY` statement to point to the new version of the .NET Framework assembly in the following folder:

`%Windir%\Microsoft.NET\Framework\Version`

> [!NOTE]
> **Version** represents the version of the .NET Framework that you installed or updated.

### Action 2

To work around scenario 2 in the [Explanation](#explanation) section, use the `ALTER ASSEMBLY` statement to update the assembly in the database.

If the problem still exists after you do this, drop the assembly from the database, and then register the new version of the assembly in the database.

## More information

We do not recommend that you use .NET Framework assemblies that are not documented in [Support policy for untested .NET Framework assemblies in the SQL Server CLR-hosted environment](/troubleshoot/sql/database-design/policy-untested-net-framework-assemblies). It lists the assemblies that are tested in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CLR-hosted environment.

### Description of CLR routines

CLR routines include the following objects that are implemented by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] integration with the .NET Framework CLR:

- Scalar-valued user-defined functions (scalar UDFs)
- Table-valued user-defined functions (TVFs)
- User-defined procedures (UDPs)
- User-defined triggers
- User-defined data types
- User-defined aggregates

### Assemblies to update after you install the .NET Framework 3.5

After you install the .NET Framework 3.5, you must use the ALTER ASSEMBLY statement to update the following assemblies:

- Accessibility.dll
- AspNetMMCExt.dll
- Cscompmgd.dll
- IEExecRemote.dll
- IEHost.dll
- IIEHost.dll
- Microsoft.Build.Conversion.dll
- Microsoft.Build.Engine.dll
- Microsoft.Build.Framework.dll
- Microsoft.Build.Tasks.dll 
- Microsoft.Build.Utilities.dll 
- Microsoft.CompactFramework.Build.Tasks.dll 
- Microsoft.JScript.dll 
- Microsoft.VisualBasic.Vsa.dll 
- Microsoft.Vsa.dll 
- Microsoft.Vsa.Vb.CodeDOMProcessor.dll 
- Microsoft_VsaVb.dll 
- Sysglobl.dll 
- System.Configuration.Install.dll 
- System.Design.dll 
- System.DirectoryServices.dll 
- System.DirectoryServices.Protocols.dll 
- System.Drawing.dll 
- System.Drawing.Design.dll 
- System.EnterpriseServices.dll 
- System.Management.dll 
- System.Messaging.dll 
- System.Runtime.Serialization.Formatters.Soap.dll 
- System.ServiceProcess.dll 
- System.Web.dll 
- System.Web.Mobile.dll 
- System.Web.RegularExpressions.dll 

These assemblies are in the following folder:

`%Windir%\Microsoft.NET\Framework\v2.0.50727`

### How to preserve the data from user-defined data types after you drop an assembly

If you drop an assembly that a user-defined data type from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses, you can use one of the following methods to preserve the data.

Assume that the following is the scenario:

- You create an assembly whose name is *MyAssembly.dll*.
- The MyAssembly assembly references the `System.DirectoryServices.dll` assembly.
- You have a user-defined data type whose name is *MyDateTime*.
- The *MyDateTime* data type uses the *MyAssembly.dll* assembly.
- You create a table whose name is *MyTable*.
- The *MyTable* table contains the data of the *MyDateTime* data type.

#### Method 1: Use the bcp.exe utility

1. Use the Bcp.exe utility together with the -n switch to copy the data from the MyTable table into a file. For example, run the following command at a command prompt:

    ```console
    bcp MyDatabase.dbo.MyTable out C:\MyFile.bcp -n -SSQLServerName -T
    ```

2. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Studio, follow these steps:
   1. Drop the MyTable table.
   2. Drop the MyDateTime data type.
   3. Drop the `System.DirectoryServices.dll` assembly.
   4. Drop the MyAssembly assembly.
3. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Studio, follow these steps:

   1. Register the `System.DirectoryServices.dll` assembly.
   2. Register the MyAssembly assembly.
   3. Create the MyDateTime data type.
   4. Create a new table that has the same table structure as the MyTable table.
4. Use the Bcp.exe utility together with the -n switch to import the data from the file into the MyTable table. For example, run the following command at a command prompt:
  
    ```console
    bcp MyDatabase.dbo.MyTable in C:\MyFile.bcp -n -SSQLServerName -T
    ```

#### Method 2: Use the INSERT ... SELECT statement

Assume that the MyDateTime data type occupies 9 bytes in storage.

1. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Studio, create a new table that contains a column of the `VARBINARY(9)` data type by running the following statement:

    ```sql
    CREATE TABLE TempTable (c1 VARBINARY(9));
    ```

2. Run the following INSERT ... SELECT statement to populate the TempTable table:

    ```sql
    INSERT INTO TempTable SELECT CAST(c1 as VARBINARY(9)) FROM MyTable;
    ```

3. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Studio, follow these steps:
   1. Drop the MyTable table.
   2. Drop the MyDateTime data type.
   3. Drop the System.DirectoryServices.dll assembly.
   4. Drop the MyAssembly assembly.
4. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Studio, follow these steps:
   1. Register the System.DirectoryServices.dll assembly.
   2. Register the MyAssembly assembly.
   3. Create the MyDateTime data type.
   4. Create a new table that has the same table structure as the MyTable table.
5. Run the following INSERT ... SELECT statement to populate the MyTable table:

    ```sql
    INSERT INTO MyTable SELECT c1 FROM TempTable;
    ```

## References

- For more information about the assembly version, see [Visual Studio 2005 Retired documentation](https://www.microsoft.com/download/details.aspx?id=55984).
- For more information about how to update an assembly, see [ALTER ASSEMBLY (Transact-SQL)](../../t-sql/statements/alter-assembly-transact-sql.md).
- For more information about how to drop an assembly, see [DROP ASSEMBLY (Transact-SQL)](../../t-sql/statements/drop-assembly-transact-sql.md).
- For more information about how to register an assembly in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, see [CREATE ASSEMBLY (Transact-SQL)](../../t-sql/statements/create-assembly-transact-sql.md).
- For more information about the Bcp.exe utility, see [https://msdn2.microsoft.com/library/ms162802.aspx](../../tools/bcp-utility.md).
