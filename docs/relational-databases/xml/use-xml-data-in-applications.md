---
title: "Use XML Data in Applications"
description: Learn about the options that are available for working with the xml data type in your applications.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "parameters [XML in SQL Server]"
  - "XML [SQL Server], ADO"
  - "columns [XML in SQL Server], ADO.NET"
  - "ADO [XML in SQL Server]"
  - "columns [XML in SQL Server], SQL Server Native Client"
  - "xml data type [SQL Server], ADO"
  - "SQLNCLI, XML"
  - "xml data type [SQL Server], SQL Server Native Client"
  - "SQL Server Native Client, XML"
  - "ADO.NET [XML in SQL Server]"
  - "XML [SQL Server], ADO.NET"
  - "columns [XML in SQL Server], ADO"
  - "xml data type [SQL Server], ADO.NET"
  - "XML [SQL Server], SQL Server Native Client"
author: MikeRayMSFT
ms.author: mikeray
---
# Use XML data in applications

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes the options that are available to you for working with the **xml** data type in your application. The article includes information about the following:

- Handling XML from an **xml** type column by using ADO and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client

- Handling XML from an **xml** type column by using ADO.NET

- Handling **xml** type in parameters by using ADO.NET

## Handle XML from an xml type column by using ADO and SQL Server Native Client

To use MDAC components to access the types and features that were introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], you must set the DataTypeCompatibility initialization property in the ADO connection string.

For example, the following Visual Basic Scripting Edition (VBScript) sample shows the results of querying an **xml** data type column, `Demographics`, in the `Sales.Store` table of the `AdventureWorks2012` sample database. Specifically, the query looks for the instance value of this column for the row where the `CustomerID` is equal to `3`.

```vb
Const DS = "MyServer"
Const DB = "AdventureWorks2012"

Set objConn = CreateObject("ADODB.Connection")
Set objRs = CreateObject("ADODB.Recordset")

CommandText = "SELECT Demographics" & _
              " FROM Sales.Store" & _
              " INNER JOIN Sales.Customer" & _
              " ON Sales.Store.BusinessEntityID = sales.customer.StoreID" & _
              " WHERE Sales.Customer.CustomerID = 3" & _
              " OR Sales.Customer.CustomerID = 4"

ConnectionString = "Provider=MSOLEDBSQL" & _
                   ";Data Source=" & DS & _
                   ";Initial Catalog=" & DB & _
                   ";Integrated Security=SSPI;" & _
                   "DataTypeCompatibility=80"

'Connect to the data source.
objConn.Open ConnectionString

'Execute command through the connection and display
Set objRs = objConn.Execute(CommandText)

Dim rowcount
rowcount = 0
Do While Not objRs.EOF
   rowcount = rowcount + 1
   MsgBox "Row " & rowcount & _
           vbCrLf & vbCrLf & objRs(0)
   objRs.MoveNext
Loop

'Clean up.
objRs.Close
objConn.Close
Set objRs = Nothing
Set objConn = Nothing
```

This example shows how to set the data type compatibility property. By default, this is set to 0 when you're using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client. If you set the value to 80, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client provider will make **xml** and user-defined type columns appear as [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] data types. This would be DBTYPE_WSTR and DBTYPE_BYTES, respectively.

> [!NOTE]
> [!INCLUDE[snac-removed-oledb-and-odbc](../../includes/snac-removed-oledb-and-odbc.md)]

### Test this example

1. Verify that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client is installed and that MDAC 2.6.0or later is available on the client computer.

     For more information, see [SQL Server Native Client Programming](../../relational-databases/native-client/sql-server-native-client-programming.md).

2. Verify that the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed.

     This example requires the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database.

3. Copy the code shown previously in this article and paste the code into your text or code editor. Save the file as HandlingXmlDataType.vbs.

4. Modify the script as required for your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation and save your changes.

     For example, where `MyServer` is specified, you should replace it with either `(local)` or the actual name of the server on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed.

5. Run HandlingXmlDataType.vbs and execute the script.

The results should be similar to the following sample output:

```xml
Row 1

<StoreSurvey xmlns="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/StoreSurvey">
  <AnnualSales>1500000</AnnualSales>
  <AnnualRevenue>150000</AnnualRevenue>
  <BankName>Primary International</BankName>
  <BusinessType>OS</BusinessType>
  <YearOpened>1974</YearOpened>
  <Specialty>Road</Specialty>
  <SquareFeet>38000</SquareFeet>
  <Brands>3</Brands>
  <Internet>DSL</Internet>
  <NumberEmployees>40</NumberEmployees>
</StoreSurvey>

Row 2

<StoreSurvey xmlns="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/StoreSurvey">
  <AnnualSales>300000</AnnualSales>
  <AnnualRevenue>30000</AnnualRevenue>
  <BankName>United Security</BankName>
  <BusinessType>BM</BusinessType>
  <YearOpened>1976</YearOpened>
  <Specialty>Road</Specialty>
  <SquareFeet>6000</SquareFeet>
  <Brands>2</Brands>
  <Internet>DSL</Internet>
  <NumberEmployees>5</NumberEmployees>
</StoreSurvey>
```

## Handle XML from an xml type column by using ADO.NET

To handle XML from an **xml** data type column by using ADO.NET and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] you can use the standard behavior of the **SqlCommand** class. For example, an **xml** data type column and its values can be retrieved in the same way any SQL column is retrieved by using a **SqlDataReader**.However, if you want to work with the contents of an **xml** data type column as XML, you'll first have to assign the contents to an **XmlReader** type.

For more information and example code, see "XML Column Values in a Data Reader" in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] 2.0 SDK documentation.

## Handle an xml type column in parameters by Using ADO.NET

To handle an **xml** data type passed as a parameter in ADO.NET and the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)], you can supply the value as an instance of the **SqlXml** data type. No special handling is involved, because **xml** data type columns in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can accept parameter values in the same way as other columns and data types, such as **string** or **integer**.

For more information and example code, see "XML Values as Command Parameters" in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] 2.0 SDK documentation.

## See also

- [XML Data &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-sql-server.md)
