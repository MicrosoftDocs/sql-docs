---
title: "Using ADO with OLE DB Driver for SQL Server | Microsoft Docs"
description: "Using ADO with OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "06/12/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "OLE DB Driver for SQL Server, ADO"
  - "data access [OLE DB Driver for SQL Server], ADO"
  - "ADO [OLE DB Driver for SQL Server]"
  - "MSOLEDBSQL, ADO"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Using ADO with OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  In order to take advantage of new features introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] such as multiple active result sets (MARS), query notifications, user-defined types (UDTs), or the new **xml** data type, existing applications that use ActiveX Data Objects (ADO) should use the OLE DB Driver for SQL Server as their data access provider.  
  
 To enable ADO to use new features of recent versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], some enhancements have been made to the OLE DB Driver for SQL Server which extends the core features of OLE DB. These enhancements allow ADO applications to use newer [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] features and to consume two data types introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]: **xml** and **udt**. These enhancements also exploit enhancements to the **varchar**, **nvarchar**, and **varbinary** data types. OLE DB Driver for SQL Server adds the SSPROP_INIT_DATATYPECOMPATIBILITY initialization property to the DBPROPSET_SQLSERVERDBINIT property set for use by ADO applications so that the new data types are exposed in a way compatible with ADO. In addition, the OLE DB Driver for SQL Server also defines a new connection string keyword named **DataTypeCompatibility** that is set in the connection string.  

> [!NOTE]  
>  Existing ADO applications can access and update XML, UDT, and large value text and binary field values using the SQLOLEDB provider. The new larger **varchar(max)**, **nvarchar(max)**, and **varbinary(max)** data types are returned as the ADO types **adLongVarChar**, **adLongVarWChar** and **adLongVarBinary** respectively. XML columns are returned as **adLongVarChar**, and UDT columns are returned as **adVarBinary**. However, if you use the OLE DB Driver for SQL Server (MSOLEDBSQL) instead of SQLOLEDB, you need to make sure to set the **DataTypeCompatibility** keyword to "80" so that the new data types will map correctly to the ADO data types.  

## Enabling OLE DB Driver for SQL Server from ADO  
 To enable the usage of OLE DB Driver for SQL Server, ADO applications will need to implement the following keywords in their connection strings:  

-   `Provider=MSOLEDBSQL`  

-   `DataTypeCompatibility=80`  

 For more information about the ADO connections string keywords supported in OLE DB Driver for SQL Server, see [Using Connection String Keywords with OLE DB Driver for SQL Server](../../oledb/applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md).  

 The following is an example of establishing an ADO connection string that is fully enabled to work with OLE DB Driver for SQL Server, including the enabling of the MARS feature:  

```  
Dim con As New ADODB.Connection  

con.ConnectionString = "Provider=MSOLEDBSQL;" _  
         & "Server=(local);" _  
         & "Database=AdventureWorks;" _   
         & "Integrated Security=SSPI;" _  
         & "DataTypeCompatibility=80;" _  
         & "MARS Connection=True;"  
con.Open  
```  

## Examples  
 The following sections provide examples of how you can use ADO with the OLE DB Driver for SQL Server.  

### Retrieving XML Column Data  
 In this example, a recordset is used to retrieve and display the data from an XML column in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **AdventureWorks** sample database.  

```  
Dim con As New ADODB.Connection  
Dim rst As New ADODB.Recordset  
Dim sXMLResult As String  

con.ConnectionString = "Provider=MSOLEDBSQL;" _  
         & "Server=(local);" _  
         & "Database=AdventureWorks;" _   
         & "Integrated Security=SSPI;" _   
         & "DataTypeCompatibility=80;"  

con.Open  

' Get the xml data as a recordset.  
Set rst.ActiveConnection = con  
rst.Source = "SELECT AdditionalContactInfo FROM Person.Contact " _  
   & "WHERE AdditionalContactInfo IS NOT NULL"  
rst.Open  

' Display the data in the recordset.  
While (Not rst.EOF)  
   sXMLResult = rst.Fields("AdditionalContactInfo").Value  
   Debug.Print (sXMLResult)  
   rst.MoveNext  
End While  

con.Close  
Set con = Nothing  
```  

> [!NOTE]  
>  Recordset filtering is not supported with XML columns. If used, an error will be returned.  

### Retrieving UDT Column Data  
 In this example, a **Command** object is used to execute a SQL query that returns a UDT, the UDT data is updated, and then the new data is inserted back into the database. This example assumes that the **Point** UDT has already been registered in the database.  

```  
Dim con As New ADODB.Connection  
Dim cmd As New ADODB.Command  
Dim rst As New ADODB.Recordset  
Dim strOldUDT As String  
Dim strNewUDT As String  
Dim aryTempUDT() As String  
Dim strTempID As String  
Dim i As Integer  

con.ConnectionString = "Provider=MSOLEDBSQL;" _  
         & "Server=(local);" _  
         & "Database=AdventureWorks;" _   
         & "Integrated Security=SSPI;" _  
         & "DataTypeCompatibility=80;"  

con.Open  

' Get the UDT value.  
Set cmd.ActiveConnection = con  
cmd.CommandText = "SELECT ID, Pnt FROM dbo.Points.ToString()"  
Set rst = cmd.Execute  
strTempID = rst.Fields(0).Value  
strOldUDT = rst.Fields(1).Value  

' Do something with the UDT by adding i to each point.  
arytempUDT = Split(strOldUDT, ",")  
i = 3  
strNewUDT = LTrim(Str(Int(aryTempUDT(0)) + i)) + "," + _  
   LTrim(Str(Int(aryTempUDT(1)) + i))  

' Insert the new value back into the database.  
cmd.CommandText = "UPDATE dbo.Points SET Pnt = '" + strNewUDT + _  
   "' WHERE ID = '" + strTempID + "'"  
cmd.Execute  

con.Close  
Set con = Nothing  
```  

### Enabling and Using MARS  
 In this example, the connection string is constructed to enable MARS through the OLE DB Driver for SQL Server, and then two recordset objects are created to execute using the same connection.  

```  
Dim con As New ADODB.Connection  

con.ConnectionString = "Provider=MSOLEDBSQL;" _  
         & "Server=(local);" _  
         & "Database=AdventureWorks;" _   
         & "Integrated Security=SSPI;" _  
         & "DataTypeCompatibility=80;" _  
         & "MARS Connection=True;"  
con.Open  

Dim recordset1 As New ADODB.Recordset  
Dim recordset2 As New ADODB.Recordset  

Dim recordsaffected As Integer  
Set recordset1 =  con.Execute("SELECT * FROM Table1", recordsaffected, adCmdText)  
Set recordset2 =  con.Execute("SELECT * FROM Table2", recordsaffected, adCmdText)  

con.Close  
Set con = Nothing  
```  

 In prior versions of the OLE DB provider, this code would cause an implicit connection to be created on the second execution because only one active set of results could be opened per a single connection. Because the implicit connection was not pooled in the OLE DB connection pool this would cause additional overhead. With the MARS feature exposed by the OLE DB Driver for SQL Server, you get multiple active results on the one connection.  

## See Also  
 [Building Applications with OLE DB Driver for SQL Server](../../oledb/applications/building-applications-with-oledb-driver-for-sql-server.md)  
