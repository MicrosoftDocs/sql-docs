---
title: "Members Example (VBScript) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "Members collection [ADO MD], VBScript example"
ms.assetid: 87bbd4ad-bb1a-4123-93ef-99ef47fd970b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Members Example (VBScript)
This sample uses an MDX query string to retrieve OLAP data and writes the resulting cellset to an HTML table structure using column spanning features for multiple-dimension cellsets.  
  
```  
<%@ Language=VBScript %>  
<%  
'************************************************************************  
'*** Active Server Page displays OLAP data from default or provided  
'*** MDX Query string and writes resulting cell set to HTML table  
'*** structure. This ASP provides colspan features for multiple  
'*** dimension cell sets.  
'************************************************************************  
Response.Buffer=True  
Response.Expires=0  
%>  
<html>  
<head>  
<meta NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">  
</head>  
<body bgcolor="Ivory">  
<font FACE="Verdana">  
  
<%  
  
Dim cat,cst,i,j,strSource,csw,LevelValue,intDC0,intDC1,intPC0, intPC1  
'************************************************************************  
'*** Gather Server Name and MDX Query Strings from text box and  
'*** text area and assign them to Session Objects of same name  
'************************************************************************  
Session("ServerName")=Request.Form("strServerName")  
Session("InitialCatalog")=Request.Form("strInitialCatalog")  
Session("MDXQuery")=Request.Form("MDXQuery")  
  
'************************************************************************  
'*** Set Connection Objects for Multi dimensional Catalog and Cell Set  
'************************************************************************  
Set cat = Server.CreateObject("ADOMD.Catalog")  
Set cst = Server.CreateObject("ADOMD.CellSet")  
  
'************************************************************************  
'*** Check to see if the Session Object Server Name is present  
'*** If present then: Create Active Connection using Server Name  
'*** and MSOLAP as connection Provider  
'*** If not present then: Use default settings of a known OLAP Server  
'*** for Server Name for Connection Set Server Name Session Object  
'*** to default value  
'************************************************************************  
If Len(Session("ServerName")) > 0 Then  
   cat.ActiveConnection = "Data Source=" & Session("ServerName") & _  
      ";Initial Catalog=" & Session("InitialCatalog") & _  
      ";Provider=msolap;"  
Else  
  
'************************************************************************  
'*** Must set OLAPServerName to OLAP Server that is  
'*** present on network  
'************************************************************************  
   OLAPServerName = "Please set to present OLAP Server"  
   cat.ActiveConnection = "Data Source=" & OLAPServerName & _  
      ";Initial Catalog=FoodMart;Provider=msolap;"  
   Session("ServerName") = OLAPServerName  
   Session("InitialCatalog") = "FoodMart"  
End if  
'************************************************************************  
'*** Check to see if the Session Object MDXQuery is present  
'*** If present then: Set strSource using MDXQuery Session Object  
'*** If not present then: Use default MDX Query string of a known query  
'*** that works with default server Set MDXQuery Session Object to   
'*** default value  
'************************************************************************  
If Len(Session("MDXQuery")) < 5 Then  
   strSource = strSource & "SELECT "  
   strSource = strSource & "CROSSJOIN({[Store].[Store Country].MEMBERS},"  
   strSource = strSource & "{[Measures].[Store " & _  
      "Invoice],[Measures].[Supply Time]}) ON COLUMNS,"  
   strSource = strSource & "CROSSJOIN({[Time].[Year].MEMBERS},"  
   strSource = strSource & "CROSSJOIN({[Store Type].[Store " & _  
      "Type].Members},{[Product].[Product Family].members})) ON ROWS"  
   strSource = strSource & " FROM Warehouse"  
Else  
   strSource = Session("MDXQuery")  
End if  
  
'************************************************************************  
'*** Set Cell Set Source property to strSource to be passed on cell set   
'*** open method  
'************************************************************************  
   cst.Source = strSource  
  
'************************************************************************  
'*** Set Cell Sets Active connection to use the current Catalogs Active   
'*** connection  
'************************************************************************  
Set cst.ActiveConnection = cat.ActiveConnection  
  
'************************************************************************  
'*** Using Open method, Open cell set  
'************************************************************************  
cst.Open  
  
'************************************************************************  
'*** Standard HTML to collect Server Name and MDX Query Information  
'*** Note that post action posts back to same page to process  
'*** thus using state of Session Variables to change look of page  
'************************************************************************  
%>  
<form action="ASPADOComplex.asp" method="POST" id="form1" name="form1">  
<table>  
<tr><td align="left">  
<b>Olap Server name:</b><br><input type="text" id="strServerName" name="strServerName" value="<%=Session("ServerName")%>" size="20">  
<br>  
<b>Catalog name:</b><br><input type="text" id="strInitialCatalog" name="strInitialCatalog" value="<%=Session("InitialCatalog")%>" size="20">  
</td><td align="center">  
<b>MDX Query:</b><br>  
<textarea rows="7" cols="70" id="textareaMDX" name="MDXQuery" wrap="soft">  
<%=Session("MDXQuery")%>  
</textarea>  
</td></tr>  
</table>  
<table>  
<tr><td>  
<input type="submit" value="Submit MDX Query" id="submit1" name="submit1">  
</td><td>  
<input type="reset" value="Reset" id="reset1" name="reset1">  
</td></tr>  
</table>  
</form>  
<p align="left">  
<font color="Black" size="-3">  
<%=strSource%>  
</font>  
</p>  
<%  
'************************************************************************  
'*** Set Dimension Counts minus 1 for Both Axes to intDC0, intDC1  
'*** Set Position Counts minus 1 for Both Axes to intPC0, intPC1  
'************************************************************************  
intDC0 = cst.Axes(0).DimensionCount-1  
intDC1 = cst.Axes(1).DimensionCount-1  
  
intPC0 = cst.Axes(0).Positions.Count - 1  
intPC1 = cst.Axes(1).Positions.Count - 1  
  
'************************************************************************  
'*** Create HTML Table structure to hold MDX Query return Record set  
'************************************************************************  
Response.Write "<Table width=100% border=1>"  
  
'************************************************************************  
'*** Loop to create Column header for all Dimensions based  
'*** on Count of Dimensions for Axes(0)  
'************************************************************************  
For h=0 to intDC0  
   Response.Write "<TR>"  
  
'************************************************************************  
'*** Loop to create spaces in front of Column headers  
'*** to align with Row headers  
'************************************************************************  
   For c=0 to intDC1  
      Response.Write "<TD></TD>"  
   Next  
  
'************************************************************************  
'*** Check current dimension to see if equal to Last Dimension  
'*** If True: Write Table header titles normally to HTML output with out   
'*** ColSpan value   
'*** If False: Write Table header titles with ColSpan values to HTML   
'*** output  
'************************************************************************  
   If h = intDC0 then  
  
'************************************************************************  
'*** Iterate through Axes(0) Positions writing member captions to table   
'*** header  
'************************************************************************  
      For i = 0 To intPC0  
         Response.Write "<TH>"  
         Response.Write "<FONT size=-2>"  
         Response.Write cst.Axes(0).Positions(i).Members(h).Caption  
         Response.Write "</FONT>"  
         Response.Write "</TH>"  
      Next  
   Else  
  
'************************************************************************  
'*** Iterate through Axes(0) Positions writing member captions to table   
'*** header taking into account for the span of columns for duplicate   
'*** member captions  
'************************************************************************  
      CaptionCount = 1  
      LastCaption = cst.Axes(0).Positions(0).Members(h).Caption  
      Response.Write "<TH"  
      For t=1 to intPC0  
  
'************************************************************************  
'*** Check to see if LastCaption is equal to current members caption  
'*** If True: Add one to CaptionCount to increase Colspan value  
'*** If False: Write Table header titles with ColSpan values to HTML   
'*** output using current CaptionCount for Colspan and LastCaption for   
'*** header string  
'************************************************************************  
         If LastCaption = _  
            cst.Axes(0).Positions(t).Members(h).Caption then  
            CaptionCount = CaptionCount+1  
  
'************************************************************************  
'*** Check if at last position  
'*** If True: Write HTML to finish table row using current  
'*** CaptionCount and LastCaption  
'************************************************************************  
            If t = intPC0 then  
               Response.Write " colspan=" & CaptionCount & _  
                  "><FONT size=-2>" & LastCaption & "</FONT></TH>"  
            End if  
  
         Else  
            Response.Write " colspan=" & CaptionCount & _  
               "><FONT size=-2>" & LastCaption & "</FONT></TH><TH"  
            CaptionCount = 1  
            LastCaption=cst.Axes(0).Positions(t).Members(h).Caption  
         End if  
      Next  
         End if  
         Response.Write "</TR>"  
      Next  
  
'************************************************************************  
'*** Iterate through Axes(1) Positions first writing member captions   
'*** to table row headers then writing cell set data to table structure  
'************************************************************************  
      Dim aryRows()  
      Dim intArray,Marker  
      intArray=0  
  
'************************************************************************  
'*** Set value of Array for row header formatting  
'************************************************************************  
      For a=1 To intDC1  
         intArray = intArray+(intPC1+1)  
      Next  
      intArray = intArray-1  
      ReDim aryRows(intArray)  
      Marker=0  
  
'************************************************************************  
'*** Use Array values for row header formatting to provide  
'*** spaces under beginning row header titles  
'************************************************************************  
      For j = 0 To intPC1  
         Response.Write "<TR>"  
         For h=0 to intDC1  
            If h=intDC1 then  
               Response.Write "<TD><B>"  
               Response.Write "<FONT size=-2>"  
               Response.Write cst.Axes(1).Positions(j).Members(h).Caption  
               Response.Write "</FONT>"  
               Response.Write "</B></TD>"  
            Else  
               aryRows(Marker) = _  
                  cst.Axes(1).Positions(j).Members(h).Caption  
               If Marker < intDC1 then  
                  Response.Write "<TD><B>"  
                  Response.Write "<FONT size=-2>"  
                  Response.Write _  
                     cst.Axes(1).Positions(j).Members(h).Caption  
                  Response.Write "</FONT>"  
                  Response.Write "</B></TD>"  
                  Marker = Marker + 1  
               Else  
                  If aryRows(Marker) = aryRows(Marker - intDC1) then  
                     Response.Write "<TD> </TD>"  
                     Marker = Marker + 1  
                  Else  
                     Response.Write "<TD><B>"  
                     Response.Write "<FONT size=-2>"  
                     Response.Write _  
                        cst.Axes(1).Positions(j).Members(h).Caption  
                     Response.Write "</FONT>"  
                     Response.Write "</B></TD>"  
                     Marker = Marker + 1  
                  End if  
               End if  
            End if  
         Next  
  
'************************************************************************  
'*** Alternates Cell background color  
'************************************************************************  
         If (j+1) Mod 2 = 0 Then  
            csw = "#cccccc"  
         Else  
            csw = "#ccffff"  
         End If  
         For k = 0 To intPC0  
            Response.Write "<TD align=right bgcolor="  
            Response.Write csw  
            Response.Write ">"  
            Response.Write "<FONT size=-2>"  
  
'************************************************************************  
'*** FormattedValue property pulls data  
'************************************************************************  
            Response.Write cst(k, j).FormattedValue  
            Response.Write "</FONT>"  
            Response.Write "</TD>"  
         Next  
         Response.Write "</TR>"  
      Next  
      Response.Write "</Table>"  
  
%>  
</font>  
</body>  
</html>  
```
