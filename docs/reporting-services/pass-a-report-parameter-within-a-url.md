---
title: "Pass a report parameter within a URL"
description: Learn about how you can pass report parameters directly to the SQL Server Reporting Services (SSRS) report server in a URL.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/17/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "URL access [Reporting Services], passing parameters"
  - "passing parameters [Reporting Services]"
#customer intent: As a SQL Server report user or administrator, I want to pass parameters within a URL so that I can customize report output and streamline report generation in SSRS.
---
# Pass a report parameter within a URL

You can pass report parameters to a SQL Server Reporting Services (SSRS) report server by including them in a report URL. All query parameters can have corresponding report parameters. You pass a query parameter to a report by passing the corresponding report parameter. For more information, see [Build a query in the Relational Query Designer &#40;Report Builder and SSRS&#41;](../reporting-services/report-data/build-a-query-in-the-relational-query-designer-report-builder-and-ssrs.md).  


## Basic parameter syntax considerations

When you want to add parameters to a URL, consider the following guidance:

- Report parameters are case-sensitive. 
- To pass parameters in a URL that contains special characters, be sure to:   
  - Replace any space characters in the URL string with the characters `%20`.  
  - Replace any space characters in the parameter portion of the URL with a plus character `+`.  
  - Replace a semicolon in any portion of the string with the characters `%3A`.
- Browsers typically handle URL encoding automatically, so you don't need to encode characters manually. 
- It's important the URL include the `_vti_bin` proxy syntax to route the request through SharePoint and the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] HTTP proxy. The proxy adds some context to the HTTP request, context that is required to ensure proper execution of the report for SharePoint mode report servers. If you don't include the proxy syntax, then you need to prefix the parameter with `rp:`.  

## Examples

> [!NOTE]
> SSRS integration with SharePoint is no longer available after SQL Server 2016. 

|Scenario|Example| 
|--------|-------|
| Set a report parameter within a URL. | `https://myrshost/ReportServer?/AdventureWorks2022/Employee_Sales_Summary_2022&TotalOrders=500`|
| Specify two parameters defined in a report on an SSRS SharePoint mode server | `https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/AdventureWorks2022/Employee_Sales_Summary_2022.rdl&ReportMonth=3&ReportYear=2008` - Example for an SSRS Native mode server. `https://myrshost/ReportServer?/AdventureWorks2022/Employee_Sales_Summary_2022&ReportMonth=3&ReportYear=2008`  |
| Specify two parameters defined in a report on an SSRS Native mode server. | `https://myrshost/ReportServer?/AdventureWorks2022/Employee_Sales_Summary_2022&ReportMonth=3&ReportYear=2008`  |
| Pass a null value for a parameter. | `https://myrshost/ReportServer?/AdventureWorks2022/Employee_Sales_Summary_2022&SalesOrderNumber:isnull=true` |
| Pass a Boolean value. Possible values are `0` for false and `1` for true. | `https://myrshost/ReportServer?/AdventureWorks2022/Employee_Sales_Summary_2022&ShowDetails=1`  |
| Pass a float value. Include the decimal separator of the server locale. | `https://myrshost/ReportServer?/AdventureWorks2022/Employee_Sales_Summary_2022&DiscountRate=0.05` |

- Set a report parameter within a URL.
    - Syntax: `parameter=value`
- Specify two parameters defined in a report.
    - Example for an SSRS SharePoint mode server: 
    `https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/AdventureWorks2022/Employee_Sales_Summary_2022.rdl&ReportMonth=3&ReportYear=2008`  
    - Example for an SSRS Native mode server: 
    `https://myrshost/ReportServer?/AdventureWorks2022/Employee_Sales_Summary_2022&ReportMonth=3&ReportYear=2008`  
- Pass a null value for a parameter. 
    - Syntax: `parameter:isnull=true` 
    - Example: `SalesOrderNumber:isnull=true` 
- Pass a Boolean value. Possible values are `0` for false and `1` for true.
    - Example: `ShowDetails=1` 
- Pass a float value. Include the decimal separator of the server locale. 
    - Example: `https://myrshost/ReportServer?/AdventureWorks2022/Employee_Sales_Summary_2022&DiscountRate=0.05`
  
## Related content

- [URL access &#40;SSRS&#41;](../reporting-services/url-access-ssrs.md)   
- [URL access parameter reference](../reporting-services/url-access-parameter-reference.md)  
  
  
