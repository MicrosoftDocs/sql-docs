---
title: "Use FOR XML Results in Application Code"
description: Learn how to use the results of a SQL query with FOR XML clauses in an application.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 03/17/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: xml
ms.topic: how-to
ms.custom:
  - sfi-ropc-nochange
  - ignite-2025
helpviewer_keywords:
  - "FOR XML clause, application code usage"
  - "XML [SQL Server], FOR XML clause"
  - "ASP.NET [SQL Server]"
  - ".NET Framework [SQL Server], FOR XML data"
  - "ADO [SQL Server]"
  - "XML data islands [SQL Server]"
  - "data islands [SQL Server]"
---
# Use FOR XML Results in application code

[!INCLUDE [SQL Server Azure SQL Database FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

By using FOR XML clauses with SQL queries, you can retrieve and cast query results as XML data. When you use FOR XML query results in XML application code, you can:

- Query SQL tables for instances of [XML Data &#40;SQL Server&#41;](../../relational-databases/xml/xml-data-sql-server.md) values

- Apply the [TYPE Directive in FOR XML Queries](../../relational-databases/xml/type-directive-in-for-xml-queries.md) to return the result of queries that contain text or image typed data as XML

This article provides examples that demonstrate these approaches.

## Retrieve FOR XML data with ADO and XML data islands

The ADO **Stream** object or other objects that support the COM **IStream** interface, such as the Active Server Pages (ASP) **Request** and **Response** objects, can contain the results when you're working with FOR XML queries.

For example, the following ASP code shows the results of querying an **xml** data type column, Demographics, in the `Sales.Store` table of the AdventureWorks sample database. Specifically, the query looks for the instance value of this column for the row where the CustomerID is equal to 3.

```vbscript
<!-- BeginRecordAndStreamVBS -->
<%@ LANGUAGE = VBScript %>
<!-- %  Option Explicit  % -->
<!-- 'Request.ServerVariables("SERVER_NAME") & ";" & _ -->
<HTML>
<HEAD>
<META NAME="GENERATOR" Content="Microsoft Developer Studio"/>
<META HTTP-EQUIV="Content-Type" content="text/html"; charset="iso-8859-1">
<TITLE>FOR XML Query Example</TITLE>
<STYLE>
   BODY
   {
      FONT-FAMILY: Tahoma;
      FONT-SIZE: 8pt;
      OVERFLOW: auto
   }
   H3
   {
      FONT-FAMILY: Tahoma;
      FONT-SIZE: 8pt;
      OVERFLOW: auto
   }
</STYLE>

<!-- #include file="adovbs.inc" -->
<%
   Response.Write "<H3>Server-side processing</H3>"
   Response.Write "Page Generated @ " & Now() & "<BR/>"
   Dim adoConn
   Set adoConn = Server.CreateObject("ADODB.Connection")
   Dim sConn
   sConn = "Provider=SQLOLEDB;Data Source=(local);" & _
            "Initial Catalog=AdventureWorks;Integrated Security=SSPI;"
   Response.write "Connect String = " & sConn & "<BR/>"
   adoConn.ConnectionString = sConn
   adoConn.CursorLocation = adUseClient
   adoConn.Open
   Response.write "ADO Version = " & adoConn.Version & "<BR/>"
   Response.write "adoConn.State = " & adoConn.State & "<BR/>"
   Dim adoCmd
   Set adoCmd = Server.CreateObject("ADODB.Command")
   Set adoCmd.ActiveConnection = adoConn
   Dim sQuery
   sQuery = "<ROOT xmlns:sql='urn:schemas-microsoft-com:xml-sql'><sql:query>SELECT Demographics from Sales.Store WHERE CustomerID = 3 FOR XML AUTO</sql:query></ROOT>"
   Response.write "Query String = " & sQuery & "<BR/>"
   Dim adoStreamQuery
   Set adoStreamQuery = Server.CreateObject("ADODB.Stream")
   adoStreamQuery.Open
   adoStreamQuery.WriteText sQuery, adWriteChar
   adoStreamQuery.Position = 0
   adoCmd.CommandStream = adoStreamQuery
   adoCmd.Dialect = "{5D531CB2-E6Ed-11D2-B252-00C04F681B71}"
   Response.write "Pushing XML to client for processing "  & "<BR/>"
   adoCmd.Properties("Output Stream") = Response
   Response.write "<XML ID='MyDataIsle'>"
   adoCmd.Execute , , 1024
   Response.write "</XML>"
%>
<SCRIPT language="VBScript" For="window" Event="onload">
   Dim xmlDoc
   Set xmlDoc = MyDataIsle.XMLDocument
   Dim root
   Set root = xmlDoc.documentElement.childNodes.Item(0).childNodes.Item(0).childNodes.Item(0)
   For each child in root.childNodes
      dim OutputXML
      OutputXML = document.all("log").innerHTML
      document.all("log").innerHTML = OutputXML & "<LI><B>" & child.nodeName &  ":</B>  " & child.Text  & "</LI>"
   Next
   MsgBox xmlDoc.xml
</SCRIPT>
</HEAD>
<BODY>
   <H3>Client-side processing of XML Document MyDataIsle</H3>
   <UL id=log>
   </UL>
</BODY>
</HTML>
<!-- EndRecordAndStreamVBS -->
```

This example ASP page contains server-side VBScript that uses ADO to execute the FOR XML query and return the XML results in an XML data island, MyDataIsle. The browser then receives this XML data island for additional client-side processing. Additional client-side VBScript code then processes the contents of the XML data island, displaying the contents as part of the resultant DHTML and opening a message box to show the preprocessed contents of the XML data island.

### Test this example

1. Verify that IIS and the AdventureWorks sample database for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are installed.

   This example requires Internet Information Services (IIS) version 5.0 or later with ASP support enabled. The AdventureWorks sample database must also be installed.

1. Copy the preceding code example and paste it into the XML or text editor that you use. Save the file as RetrieveResults.asp in the root directory that IIS uses. Typically, this directory is C:Inetpub\wwwroot.

1. Open the ASP page in a browser window by using the following URL. First, replace 'MyServer' with either "localhost" or the actual name of the server where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and IIS are installed.

    ```text
    https://MyServer/RetrieveResults.asp
    ```

The generated HTML page results that appear will be similar to the following sample output:

#### Server-side processing

```output
Page Generated @ 3/11/2006 3:36:02 PM

Connect String = Provider=SQLOLEDB;Data Source=MyServer;Initial Catalog=AdventureWorks;Integrated Security=SSPI;

ADO Version = 2.8

adoConn.State = 1

Query String = SELECT Demographics from Sales.Store WHERE CustomerID = 3 FOR XML AUTO

Pushing XML to client for processing
```

#### Client-side processing of XML document MyDataIsle

- **AnnualSales:** 1500000

- **AnnualRevenue:** 150000

- **BankName:** Primary International

- **BusinessType:** OS

- **YearOpened:** 1974

- **Specialty:** Road

- **SquareFeet:** 38000

- **Brands:** 3

- **Internet:** DSL

- **NumberEmployees:** 40

The VBScript message box will then show the following original, unfiltered XML data island contents that were returned by the FOR XML query results.

```xml
<ROOT xmlns:sql="urn:schemas-microsoft-com:xml-sql">
  <Sales.Store>
    <Demographics>
      <StoreSurvey xmlns="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/StoreSurvey">
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
    </Demographics>
  </Sales.Store>
</ROOT>
```

## Retrieve FOR XML data with ASP.NET and the .NET Framework

As in the previous example, the following ASP.NET code shows the results of querying an **xml** data type column, Demographics, in the `Sales.Store` table of the AdventureWorks sample database. As in the previous example, the query looks for the instance value of this column for the row where the CustomerID is equal to 3.

In this example, the following Microsoft .NET Framework managed APIs return and render the FOR XML query results:

1. **SqlConnection** opens a connection to SQL Server, based on the contents of a specified connection string variable, `strConn`.

1. **SqlDataAdapter** serves as the data adapter and uses the SQL connection and a specified SQL query string to execute the FOR XML query.

1. After the query executes, the **SqlDataAdapter.Fill** method fills a **DataSet** instance, `MyDataSet`, with the output of the FOR XML query.

1. The **DataSet.GetXml** method returns the query results as a string that the server-generated HTML page displays.

    ```vb
    <%@ Page Language="VB" %>
    <HTML>
    <HEAD>
    <TITLE>FOR XML Query Example</TITLE>
    <STYLE>
       BODY
       {
          FONT-FAMILY: Tahoma;
          FONT-SIZE: 8pt;
          OVERFLOW: auto
       }
       H3
       {
          FONT-FAMILY: Tahoma;
          FONT-SIZE: 8pt;
          OVERFLOW: auto
       }
    </STYLE>
    </HEAD>
    <BODY>
    <%
    Dim s as String
    s = "<H3>Server-side processing</H3>" & _
        "Page Generated @ " & Now() & "<BR/>"

    Dim SQL As String
    SQL = "SELECT Demographics from Sales.Store WHERE CustomerID = 3 FOR XML AUTO"

    Dim strConn As String
    strConn = "Server=(local);Database=AdventureWorks;Integrated Security=SSPI;"

    Dim MySqlConn As New Microsoft.Data.SqlClient.SqlConnection(strConn)
    Dim MySqlAdapter As New Microsoft.Data.SqlClient.SqlDataAdapter(SQL,MySqlConn)
    Dim MyDataSet As New System.Data.DataSet

    MySqlConn.Open()
    s = s & "<P>SqlConnection opened.</P>"

    MySqlAdapter.Fill(MyDataSet)
    s = s & "<P>" & MyDataSet.GetXml  & "</P>"

    MySqlConn.Close()
    s = s & "<P>SqlConnection closed.</P>"

    Message.InnerHtml=s
    %>
    <SPAN id="Message" runat=server />
    </BODY>
    </HTML>
    ```

### Test this example

1. Verify that IIS and the AdventureWorks sample database for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are installed.

   This example requires Internet Information Services (IIS) version 5.0 or later with ASP.NET support enabled. The AdventureWorks sample database must also be installed.

1. Copy the preceding code and paste it into the XML or text editor that you use. Save the file as RetrieveResults.aspx in the root directory that IIS uses. Typically, this directory is C:Inetpub\wwwroot.

1. Open the ASP.NET page in a browser window by using the following URL. First, replace 'MyServer' with either "localhost" or the actual name of the server where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and IIS are installed.

    ```text
    https://MyServer/RetrieveResults.aspx
    ```

The generated HTML page results that appear will be similar to the following sample output:

#### Server-side processing

```output
Page Generated @ 3/11/2006 3:36:02 PM

SqlConnection opened.

<Sales.Store><Demographics><StoreSurvey xmlns="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/StoreSurvey"><AnnualSales>1500000</AnnualSales><AnnualRevenue>150000</AnnualRevenue><BankName>Primary International</BankName><BusinessType>OS</BusinessType><YearOpened>1974</YearOpened><Specialty>Road</Specialty><SquareFeet>38000</SquareFeet><Brands>3</Brands><Internet>DSL</Internet><NumberEmployees>40</NumberEmployees></StoreSurvey></Demographics></Sales.Store>

SqlConnection closed.
```

> [!NOTE]
> The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **xml** data type support lets you request that a FOR XML query returns results as **xml** data type, instead of as string or image typed data, by specifying the [TYPE directive](../../relational-databases/xml/type-directive-in-for-xml-queries.md). When you use the TYPE directive in FOR XML queries, it provides programmatic access to the FOR XML results similar to the approach shown in [Use XML Data in Applications](../../relational-databases/xml/use-xml-data-in-applications.md).

## Related content

- [FOR XML (SQL Server)](for-xml-sql-server.md)
