---
title: "Migrating From ADO MD To ADOMD.NET | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "ADOMD.NET, migrating to"
  - "migrating ADO MD to ADOMD.NET"
  - "ADO MD migration [ADOMD.NET]"
ms.assetid: 8c760db3-c475-468e-948d-e5f599d985ad
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Migrating From ADO MD To ADOMD.NET
  The ADOMD.NET library is similar to the ActiveX Data Objects Multidimensional (ADO MD) library, an extension of the ActiveX Data Objects (ADO) library that is used to access multidimensional data in Component Object Model (COM)–based client applications. ADO MD provides easy access to multidimensional data from unmanaged languages such as C++ and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic. ADOMD.NET provides easy access to analytical (both multidimensional and data mining) data from managed languages such as [!INCLUDE[msCoName](../../includes/msconame-md.md)] C# and [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic .NET. Additionally, ADOMD.NET provides an enhanced metadata object model.  
  
 Migrating existing client applications from ADO MD to ADOMD.NET is easy, but there are several important differences regarding migration:  
  
 **To provide connectivity and data access to client applications**  
 |ADO MD|ADOMD.NET|  
|------------|---------------|  
|Requires references to both Adodb.dll and Adomd.dll.|Requires a single reference to Microsoft.AnalysisServices.AdomdClient.dll.|  
  
 The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> class provides connectivity support, in addition to access to metadata.  
  
 **To retrieve metadata for multidimensional objects**  
 |ADO MD|ADOMD.NET|  
|------------|---------------|  
|Use the Catalog class.|Use the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.Cubes%2A> property of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection>.|  
  
 **To run queries and return cellset objects**  
 |ADO MD|ADOMD.NET|  
|------------|---------------|  
|Use the CellSet class.|Use the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> class.|  
  
 **To access the metadata that is used to display a cellset**  
 |ADO MD|ADOMD.NET|  
|------------|---------------|  
|Use the Position class.|Use the <xref:Microsoft.AnalysisServices.AdomdClient.Set> and <xref:Microsoft.AnalysisServices.AdomdClient.Tuple> objects.|  
  
> [!NOTE]  
>  The <xref:Microsoft.AnalysisServices.AdomdClient.Position> class is supported for backward compatibility.  
  
 **To retrieve mining model metadata**  
 In ADO MD there are no classes available. In  ADOMD.NET use one of the data mining collections:  
  
-   The <xref:Microsoft.AnalysisServices.AdomdClient.MiningModelCollection> contains a list of every mining model in the data source.  
  
-   The <xref:Microsoft.AnalysisServices.AdomdClient.MiningServiceCollection> provides information about the available mining algorithms.  
  
-   The <xref:Microsoft.AnalysisServices.AdomdClient.MiningStructureCollection> exposes information about the mining structures on the server.  
  
 To highlight these differences the following migration example compares an existing ADO MD application to an equivalent ADOMD.NET application.  
  
## Looking at a Migration Example  
 Both the existing ADO MD and equivalent ADOMD.NET code examples shown in this section perform the same set of actions: creating a connection, running a Multidimensional Expressions (MDX) statement, and retrieving metadata and data. However, these two sets of code do not use the same objects to perform those tasks.  
  
### Existing ADO MD Code  
 The following code example, drawn from ADO MD 2.8 documentation, is written in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic® 6.0 and uses ADO MD to demonstrate how to connect to and query a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source. This ADO MD example uses the following objects:  
  
-   Creates a connection from a **Catalog** object.  
  
-   Runs the Multidimensional Expressions (MDX) statement using the **Cellset** object.  
  
-   Retrieves the metadata and data from the **Position** object, retrieved from the **Cellset** object.  
  
```  
Private Sub cmdCellSettoDebugWindow_Click()  
Dim cat As New ADOMD.Catalog  
Dim cst As New ADOMD.Cellset  
Dim i As Integer  
Dim j As Integer  
Dim k As Integer  
Dim strServer As String  
Dim strSource As String  
Dim strColumnHeader As String  
Dim strRowText As String  
  
On Error GoTo Error_cmdCellSettoDebugWindow_Click  
Screen.MousePointer = vbHourglass  
'*-----------------------------------------------------------------------  
'* Set server to local host.  
'*-----------------------------------------------------------------------  
    strServer = "LOCALHOST"  
  
'*-----------------------------------------------------------------------  
'* Set MDX query string source.  
'*-----------------------------------------------------------------------  
    strSource = strSource & "SELECT "  
    strSource = strSource & "{[Measures].members} ON COLUMNS,"  
    strSource = strSource & _  
        "NON EMPTY [Store].[Store City].members ON ROWS"  
    strSource = strSource & " FROM Sales"  
  
'*-----------------------------------------------------------------------  
'* Set active connection.  
'*-----------------------------------------------------------------------  
        cat.ActiveConnection = "Data Source=" & strServer & _  
            ";Provider=msolap;"  
  
'*-----------------------------------------------------------------------  
'* Set cellset source to MDX query string.  
'*-----------------------------------------------------------------------  
        cst.Source = strSource  
  
'*-----------------------------------------------------------------------  
'* Set cellset active connection to current connection  
'*-----------------------------------------------------------------------  
    Set cst.ActiveConnection = cat.ActiveConnection  
  
'*-----------------------------------------------------------------------  
'* Open cellset.  
'*-----------------------------------------------------------------------  
    cst.Open  
  
'*-----------------------------------------------------------------------  
'* Allow space for row header text.  
'*-----------------------------------------------------------------------  
strColumnHeader = vbTab & vbTab & vbTab & vbTab & vbTab & vbTab  
  
'*-----------------------------------------------------------------------  
'* Loop through column headers.  
'*-----------------------------------------------------------------------  
       For i = 0 To cst.Axes(0).Positions.Count - 1  
            strColumnHeader = strColumnHeader & _  
                cst.Axes(0).Positions(i).Members(0).Caption & vbTab & _  
                    vbTab & vbTab & vbTab  
       Next  
       Debug.Print vbTab & strColumnHeader & vbCrLf  
  
'*-----------------------------------------------------------------------  
'* Loop through row headers and provide data for each row.  
'*-----------------------------------------------------------------------  
        strRowText = ""  
        For j = 0 To cst.Axes(1).Positions.Count - 1  
            strRowText = strRowText & _  
                cst.Axes(1).Positions(j).Members(0).Caption & vbTab & _  
                    vbTab & vbTab & vbTab  
            For k = 0 To cst.Axes(0).Positions.Count - 1  
                strRowText = strRowText & cst(k, j).FormattedValue & _  
                    vbTab & vbTab & vbTab & vbTab  
            Next  
            Debug.Print strRowText & vbCrLf  
            strRowText = ""  
        Next  
  
    Screen.MousePointer = vbDefault  
Exit Sub  
  
Error_cmdCellSettoDebugWindow_Click:  
   Beep  
   Screen.MousePointer = vbDefault  
   MsgBox "The following error has occurred:" & vbCrLf & _  
      Err.Description, vbCritical, " Error!"  
   Exit Sub  
End Sub  
```  
  
### Equivalent ADOMD.NET Code  
 The following example, written in Visual Basic .NET and using ADOMD.NET, demonstrates how to perform the same actions as the previous Visual Basic 6.0 example. The major difference between the following example and the ADO MD example shown earlier is the objects that are used to perform the actions. The ADOMD.NET example uses the following objects:  
  
-   Creates a connection with an **AdomdConnection** object.  
  
-   Runs the MDX statement using an **AdomdCommand** object.  
  
-   Retrieves the metadata and data from the **Set** object, retrieved from the **Cellset** object.  
  
```  
Private Sub DisplayCellSetInOutputWindow()  
    Dim conn As AdomdConnection  
    Dim cmd As AdomdCommand  
    Dim cst As CellSet  
    Dim i As Integer  
    Dim j As Integer  
    Dim k As Integer  
    Dim strServer As String = "LOCALHOST"  
    Dim strSource As String = "SELECT [Measures].members ON COLUMNS, " & _  
        "NON EMPTY [Store].[Store City].members ON ROWS FROM SALES"  
    Dim strOutput As New System.IO.StringWriter  
  
    '*-----------------------------------------------------------------------  
    '* Open connection.  
    '*-----------------------------------------------------------------------  
    Try  
        ' Create a new AdomdConnection object, providing the connection  
        ' string.  
        conn = New AdomdConnection("Data Source=" & strServer & _  
        ";Provider=msolap;")  
        ' Open the connection.  
        conn.Open()  
    Catch ex As Exception  
        Throw New ApplicationException( _  
            "An error occurred while connecting.")  
    End Try  
  
    Try  
    '*-----------------------------------------------------------------------  
    '* Open cellset.  
    '*-----------------------------------------------------------------------  
        ' Create a new AdomdCommand object, providing the MDX query string.  
        cmd = New AdomdCommand(strSource, conn)  
        ' Run the command and return a CellSet object.  
        cst = cmd.ExecuteCellSet()  
  
    '*-----------------------------------------------------------------------  
    '* Concatenate output.  
    '*-----------------------------------------------------------------------  
  
    ' Include spacing to account for row headers.  
    strOutput.Write(vbTab, 6)  
  
    ' Iterate through the first axis of the CellSet object and  
    ' retrieve column headers.  
    For i = 0 To cst.Axes(0).Set.Tuples.Count - 1  
        strOutput.Write(cst.Axes(0).Set.Tuples(i).Members(0).Caption)  
        strOutput.Write(vbTab, 4)  
    Next  
    strOutput.WriteLine()  
  
    ' Iterate through the second axis of the CellSet object and  
    ' retrieve row headers and cell data.  
    For j = 0 To cst.Axes(1).Set.Tuples.Count - 1  
        ' Append the row header.  
        strOutput.Write(cst.Axes(1).Set.Tuples(j).Members(0).Caption)  
        strOutput.Write(vbTab, 4)  
  
        ' Append the cell data for that row.  
        For k = 0 To cst.Axes(0).Set.Tuples.Count - 1  
            strOutput.Write(cst.Cells(k, j).FormattedValue)  
            strOutput.Write(vbTab, 4)  
        Next  
        strOutput.WriteLine()  
    Next  
  
    ' Display the output.  
    Debug.WriteLine(strOutput.ToString)  
  
    '*-----------------------------------------------------------------------  
    '* Release resources.  
    '*-----------------------------------------------------------------------  
        conn.Close()  
    Catch ex As Exception  
        ' Ignore or handle errors.  
    Finally  
        cst = Nothing  
        cmd = Nothing  
        conn = Nothing  
    End Try  
End Sub  
```  
  
  