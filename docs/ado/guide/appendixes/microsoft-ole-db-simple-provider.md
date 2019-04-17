---
title: "Microsoft OLE DB Simple Provider | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/08/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords:
  - "simple provider [ADO]"
  - "providers [ADO], OLE DB simple provider"
  - "OLE DB simple provider [ADO]"
ms.assetid: 1e7dc6f0-482c-4103-8187-f890865e40fc
author: MightyPen
ms.author: genemi
manager: craigg
---
# Microsoft OLE DB Simple Provider Overview
The Microsoft OLE DB Simple Provider (OSP) allows ADO to access any data for which a provider has been written using the [OLE DB Simple Provider (OSP) Toolkit](https://msdn.microsoft.com/6e7b7931-9e4a-4151-ae51-672abd3f84a6). Simple providers are intended to access data sources that require only fundamental OLE DB support, such as in-memory arrays or XML documents.

## Connection String Parameters
 To connect to the OLE DB Simple Provider DLL, set the *Provider* argument to the [ConnectionString](../../../ado/reference/ado-api/connectionstring-property-ado.md) property to:

```vb
MSDAOSP
```

 This value can also be set or read using the [Provider](../../../ado/reference/ado-api/provider-property-ado.md) property.

 You can connect to simple providers that have been registered as full OLE DB providers by using the registered provider name, determined by the provider writer.

## Typical Connection String
 A typical connection string for this provider is:

```vb
"Provider=MSDAOSP;Data Source=serverName"
```

 The string consists of these keywords:

|Keyword|Description|
|-------------|-----------------|
|**Provider**|Specifies the OLE DB provider for SQL Server.|
|**Data Source**|Specifies the name of a server.|

## XML Document Example
 The OLE DB Simple Provider (OSP) in MDAC 2.7 or later and Windows Data Access Components (Windows DAC) has been enhanced to support opening hierarchical ADO **Recordsets** over arbitrary XML files. These XML files may contain the ADO XML persistence schema, but it is not required. This has been implemented by connecting the OSP to the **MSXML2.DLL**; therefore **MSXML2.DLL** or later is required.

 The **portfolio.xml** file used in the following example contains the following tree:

```console
Portfolio
   Stock
      Shares
      Symbol
      Price
      Info
         Company Name
         WebSite
```

 The XML DSO uses built-in heuristics to convert the nodes in an XML tree to chapters in a hierarchical **Recordset**.

 Using these built-in heuristics, the XML tree is converted into a two-level hierarchical **Recordset** of the following form:

```console
Parent Recordset
Shares, Symbol, Price, $Text
   Child Recordset
      Company Name, WebSite, $Text
```

 Note that the Portfolio and Info tags are not represented in the hierarchical **Recordset**. For an explanation of how the XML DSO converts XML trees to hierarchical **Recordsets**, see the following rules. The $Text column is discussed in the following section.

## Rules for Assigning XML Elements and Attributes to Columns and Rows
 The XML DSO follows a procedure for assigning elements and attributes to columns and rows in data-bound applications. XML is modeled as a tree with one tag that contains the entire hierarchy. For example, an XML description of a book could contain chapter tags, figure tags, and section tags. At the highest level would be the book tag, containing the subelements chapter, figure, and section. When the XML DSO maps XML elements to rows and columns, the subelements, not the top level element, are converted.

 The XML DSO uses this procedure for converting the subelements:

-   Each subelement and attribute corresponds to a column in some **Recordset** in the hierarchy.

-   The name of the column is the same as the name of the subelement or attribute, unless the parent element has an attribute and a subelement with the same name, in which case a "!" is prepended to the subelement's column name.

-   Each column is either a *simple* column that contains scalar values (usually strings) or a **Recordset** column that contains child **Recordsets**.

-   Columns corresponding to attributes are always simple.

-   Columns corresponding to subelements are **Recordset** columns if either the subelement has its own subelements or attributes (or both), or the subelement's parent has more than one instance of the subelement as a child. Otherwise the column is simple.

-   When there are multiple instances of a subelement (under different parents), its column is a **Recordset** column if *any* of the instances imply a **Recordset** column; its column is simple only if *all* instances imply a simple column.

-   All **Recordsets** have an additional column named $Text.

 The code that is needed to construct a **Recordset** is as follows:

```vb
Dim adoConn as ADODB.Connection
Dim adoRS as ADODB.Recordset

Set adoRS = New ADODB.Connection
Set adoRS = New ADODB.Recordset

adoConn.Open "Provider=MSDAOSP; Data Source=MSXML2.DSOControl.2.6;"
adoRS.Open "https://WebServer/VRoot/portfolio.xml, adoConn
```

> [!NOTE]
>  The path of the data file can be specified by using four different naming conventions.

```vb
'HTTP://
adoRS.Open "https://WebServer/VRoot/portfolio.xml", adoConn
'FILE://
adoRS.Open "file:/// C:\\Directory\\portfolio.xml", adoConn
'UNC Path
adoRS.Open "\\ComputerName\ShareName\portfolio.xml", adoConn
'Full DOS Path
adoRS.Open "C:\Directory\portfolio.xml", adoConn
```

 As soon as the **Recordset** has been opened, the usual ADO **Recordset** navigation commands can be used.

 **Recordsets** generated by the OSP have a few limitations:

-   Client cursors (**adUseClient**) are not supported.

-   Hierarchical **Recordsets** created over arbitrary XML cannot be persisted using **Recordset.Save**.

-   **Recordsets** created with the OSP are read-only.

-   The XMLDSO adds an additional column of Data ($Text) to each **Recordset** in the Hierarchy.

 For more information about the OLE DB Simple Provider, see [Building a Simple Provider](https://msdn.microsoft.com/b31a6cba-58ae-4ee8-9039-700973d354d6).

## Code Example
 The following Visual Basic code demonstrates opening an arbitrary XML file, constructing a hierarchical **Recordset**, and recursively writing each record of each **Recordset** to the debug window.

 Here is a simple XML file that contains stock quotes. The following code uses this file to construct a two-level hierarchical **Recordset**.

```xml
<portfolio>
   <stock>
      <shares>100</shares>
      <symbol>MSFT</symbol>
      <price>$70.00</price>
      <info>
         <companyname>Microsoft Corporation</companyname>
         <website>https://www.microsoft.com</website>
      </info>
   </stock>
   <stock>
      <shares>100</shares>
      <symbol>AAPL</symbol>
      <price>$107.00</price>
      <info>
         <companyname>Apple Computer, Inc.</companyname>
         <website>https://www.apple.com</website>
      </info>
   </stock>
   <stock>
      <shares>100</shares>
      <symbol>DELL</symbol>
      <price>$50.00</price>
      <info>
         <companyname>Dell Corporation</companyname>
         <website>https://www.dell.com</website>
      </info>
    </stock>
    <stock>
       <shares>100</shares>
       <symbol>INTC</symbol>
       <price>$115.00</price>
       <info>
          <companyname>Intel Corporation</companyname>
          <website>https://www.intel.com</website>
       </info>
   </stock>
</portfolio>
```

 Following are two Visual Basic sub procedures. The first creates the **Recordset** and passes it to the *WalkHier* sub procedure, which recursively walks down the hierarchy, writing each **Field** in each record in each **Recordset** to the debug window.

```vb
Private Sub BrowseHierRecordset()
' Add ADO 2.7 or later to Project/References
' No need to add MSXML2, ADO just passes the ProgID through to the OSP.

    Dim adoConn As ADODB.Connection
    Dim adoRS As ADODB.Recordset
    Dim adoChildRS As ADODB.Recordset

    Set adoConn = New ADODB.Connection
    Set adoRS = New ADODB.Recordset
    Set adoChildRS = ADODB.Recordset

    adoConn.Open "Provider=MSDAOSP; Data Source=MSXML2.DSOControl.2.6;"
    adoRS.Open "https://bwillett3/Kowalski/portfolio.xml", adoConn

    Dim iLevel As Integer
    iLevel = 0
    WalkHier iLevel, adoRS

End Sub

Sub WalkHier(ByVal iLevel As Integer, ByVal adoRS As ADODB.Recordset)
    iLevel = iLevel + 1
    PriorLevel = iLevel
    While Not adoRS.EOF
        For ndx = 0 To adoRS.Fields.Count - 1
            If adoRS.Fields(ndx).Name <> "$Text" Then
                If adoRS.Fields(ndx).Type = adChapter Then
                    Set adoChildRS = adoRS.Fields(ndx).Value
                    WalkHier iLevel, adoChildRS
                Else
                    Debug.Print iLevel & ": adoRS.Fields(" & ndx & _
                       ") = " & adoRS.Fields(ndx).Name & " = " & _
                       adoRS.Fields(ndx).Value
                End If
            End If
        Next ndx
        adoRS.MoveNext
    Wend
    iLevel = PriorLevel
End Sub
```
