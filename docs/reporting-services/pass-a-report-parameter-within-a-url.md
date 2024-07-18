---
title: "Pass a report parameter within a URL"
description: Learn how to pass report parameters directly to the SQL Server Reporting Services (SSRS) report server in a URL.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/17/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: how-to
ms.custom: updatefrequency5
helpviewer_keywords:
  - "URL access [Reporting Services], passing parameters"
  - "passing parameters [Reporting Services]"
#customer intent: As a SQL Server report user or administrator, I want to pass parameters within a URL to customize report outputs and streamline report generation in SSRS.
---
# Pass a report parameter within a URL

Learn how to pass report parameters to a SQL Server Reporting Services (SSRS) report server by including them in a report URL. All query parameters can have corresponding report parameters. You pass a query parameter to a report by passing the corresponding report parameter. For more information, see [Build a query in the Relational Query Designer &#40;Report Builder and SSRS&#41;](../reporting-services/report-data/build-a-query-in-the-relational-query-designer-report-builder-and-ssrs.md).  


## Basic parameter syntax
  
To set a report parameter within a URL, use the following syntax:  
  
``` 
parameter=value  
```  

> [!NOTE]
> Report parameters are case-sensitive. 

## Pass parameters in a URL that contains special characters

To pass parameters in a URL that contains special characters, be sure to:   
- Replace any space characters in the URL string with the characters `%20`.  
- Replace space character in the parameter portion of the URL with a plus character `+`.  
- Replace a semicolon in any portion of the string with the characters `%3A`.

Browsers typically handle URL encoding automatically, so you don't need to encode characters manually. 

In this example:

- The URL, `https://myrshost/ReportServer?/AdventureWorks2022/Employee%20Sales%20Summary%202022` replaces spaces in the report path with `%20`.
- The parameter, `EmployeeName=John+Doe%3A+Manager`, replaces spaces in the parameter value with `+` and the semicolon with `%3A`.
  
```  
https://myrshost/ReportServer?/AdventureWorks2022/Employee%20Sales%20Summary%202022&EmployeeName=John+Doe%3A+Manager
``` 

## Pass `ReportMonth` and `ReportYear` parameters to an SSRS Native mode report server

To pass specific month and year parameters (`ReportMonth` and `ReportYear`) to get a filtered report based on those values, use the following syntax:   
  
```  
https://myrshost/ReportServer?/AdventureWorks2022/Employee_Sales_Summary_2022&ReportMonth=3&ReportYear=2008  
```  

## Pass `ReportMonth` and `ReportYear` to an SSRS SharePoint mode report server

> [!NOTE]
> SSRS integration with SharePoint is no longer available after SQL Server 2016.  

It's important the URL include the `_vti_bin` proxy syntax to route the request through SharePoint and the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] HTTP proxy. The proxy adds some context to the HTTP request, context that is required to ensure proper execution of the report for SharePoint mode report servers. If you don't include the proxy syntax, then you need to prefix the parameter with `rp:`.  

To specify the `ReportMonth` and `ReportYear` parameters for a similar report on an SSRS SharePoint Integrated report server, use the following syntax: 
  
```  
https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/AdventureWorks2022/Employee_Sales_Summary_2022.rdl&ReportMonth=3&ReportYear=2008  
```  
  
## Pass a null value 

To pass a null value for a parameter in a URL, use the following syntax:  
  
```  
parameter:isnull=true  
```  
  
For example,  
  
```  
https://myserver/Reportserver?/AdventureWorks2022/Order_Summary&SalesOrderNumber:isnull=true
```  
  
## Pass a boolean value

To pass a **Boolean** value in a URL, use `0` for false and `1` for true:

```
https://myrshost/ReportServer?/AdventureWorks2022/Employee_Sales_Summary_2022&ShowDetails=1
```
In this example:

- `ShowDetails=1` sets the `ShowDetails` parameter to true, indicating that the report should display detailed information.

## Pass a float value

To pass a float value in a URL, include the decimal separator of the server locale:

```
https://myrshost/ReportServer?/AdventureWorks2022/Employee_Sales_Summary_2022&DiscountRate=0.05
```

In this example:

- `DiscountRate=0.05` sets the `DiscountRate` parameter to 0.05, representing a 5% discount rate to be applied in the report.
  
> [!NOTE]  
>  If your report contains a report parameter that has a default value and the value of the **Prompt** property is **false**, then you can't pass a value for that report parameter within a URL. This option allows administrators to prevent end users from adding or modifying the values of certain report parameters.  

## Pass multi-value parameters  
  
To pass multi-value parameters in a URL, repeat the parameter name for each value, as in the following URL:  
  
```  
https://myserver/Reportserver?/SQL+Server+User+Education+Team/_ContentTeams/folder123/team+project+report&teamgrouping2=xgroup&teamgrouping1=ygroup&OrderID=747&OrderID=787&OrderID=12  
```  

In this example:

- `&OrderID=747&OrderID=787&OrderID=12` repeats the `OrderID` parameter for each value. The report receives the three `OrderID` values: 747, 787, and 12.
  
## Related content

- [URL access &#40;SSRS&#41;](../reporting-services/url-access-ssrs.md)   
- [URL access parameter reference](../reporting-services/url-access-parameter-reference.md)  
  
  
