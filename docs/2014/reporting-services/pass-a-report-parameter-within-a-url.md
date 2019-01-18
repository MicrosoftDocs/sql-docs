---
title: "Pass a Report Parameter Within a URL | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "URL access [Reporting Services], passing parameters"
  - "passing parameters [Reporting Services]"
ms.assetid: f93a94cc-27b5-435a-aa85-69e6ec6459ad
author: markingmyname
ms.author: maghan
manager: craigg
---
# Pass a Report Parameter Within a URL
  You can pass report parameters to a report by including them in a report URL. These URL parameters are not prefixed because they are passed directly to the report processing engine.  
  
> [!IMPORTANT]  
>  It is important the URL include the `_vti_bin` proxy syntax to route the request through SharePoint and the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] HTTP proxy. The proxy adds some context to the HTTP request, context that is required to ensure proper execution of the report for SharePoint mode report servers.  
>   
>  If you don't include the proxy syntax, then you need to prefix the parameter with *rp:*.  
  
 All query parameters can have corresponding report parameters. You pass a query parameter to a report by passing the corresponding report parameter. For more information, see [Build a Query in the Relational Query Designer &#40;Report Builder and SSRS&#41;](report-data/build-a-query-in-the-relational-query-designer-report-builder-and-ssrs.md).  
  
> [!IMPORTANT]
>  Report parameters are case-sensitive.  
> 
> [!NOTE]
>  Report parameters are case-sensitive and utilize the following special characters:  
> 
>  -   Any space characters in the URL string are replaced with the characters "%20," according to URL encoding standards.  
> -   A space character in the parameter portion of the URL is replaced with a plus character (+).  
> -   A semicolon in any portion of the string is replaced with the characters "%3A."  
> -   Browsers should automatically perform the proper URL encoding. You do not have to encode any of the characters manually.  
  
 To set a report parameter within a URL, use the following syntax:  
  
```  
  
parameter=value  
```  
  
 For example, to specify two parameters, "ReportMonth" and "ReportYear", defined in a report, use the following URL for a native mode report server:  
  
```  
http://myrshost/ReportServer?/AdventureWorks 2008R2/Employee_Sales_Summary_2008R2&ReportMonth=3&ReportYear=2008  
```  
  
 For example, to specify the same two parameters defined in a report, use the following URL for a SharePoint integrated mode report server. Note the `/_vti_bin`:  
  
```  
http://myspsite/subsite/_vti_bin/reportserver?http://myspsite/subsite/AdventureWorks 2008R2/Employee_Sales_Summary_2008R2.rdl&ReportMonth=3&ReportYear=2008  
```  
  
 To pass a null value for a parameter, use the following syntax:  
  
```  
  
parameter  
:isnull=true  
  
```  
  
 For example,  
  
```  
SalesOrderNumber:isnull=true  
```  
  
 To pass a `Boolean` value, use 0 for false and 1 for true. To pass a `Float` value, include the decimal separator of the server locale  
  
> [!NOTE]  
>  If your report contains a report parameter that has a default value and the value of the `Prompt` property is `false` (that is, the Prompt User property is not selected in Report Manager), then you cannot pass a value for that report parameter within a URL. This provides administrators an option for preventing end users from adding or modifying the values of certain report parameters.  
  
##  <a name="bkmk_examples"></a> Additional Examples  
 The following URL example includes spaces and multiple parameters  
  
-   Folder name of "SQL Server User Education Team" includes spaces and therefore the "+" replaces each space.  
  
-   Report name of "team project report" includes spaces and therefore the "+" replaces each space.  
  
-   Passes two parameters of "teamgrouping2" with a value of "xgroup" and "teamgrouping1" with a value of "ygroup".  
  
```  
https://myserver/Reportserver?/SQL+Server+User+Education+Team/_ContentTeams/folder123/team+project+report&teamgrouping2=xgroup&teamgrouping1=ygroup  
```  
  
 The following URL example includes a multi-value parameter "OrderID. The format for a Multi-Value parameter is to repeat the parameter name for each value.  
  
```  
https://myserver/Reportserver?/SQL+Server+User+Education+Team/_ContentTeams/folder123/team+project+report&teamgrouping2=xgroup&teamgrouping1=ygroup&OrderID=747&OrderID=787&OrderID=12  
```  
  
 The following URL example passes a single parameter of *SellStartDate* with a value of "7/1/2005", for a native mode report server.  
  
```  
http://myserver/ReportServer/Pages/ReportViewer.aspx?%2fProduct_and_Sales_Report_AdventureWorks&SellStartDate=7/1/2005  
```  
  
## See Also  
 [URL Access &#40;SSRS&#41;](url-access-ssrs.md)   
 [URL Access Parameter Reference](url-access-parameter-reference.md)  
  
  
