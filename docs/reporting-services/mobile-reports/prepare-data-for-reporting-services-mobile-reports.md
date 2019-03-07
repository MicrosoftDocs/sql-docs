---
title: "Prepare data for Reporting Services mobile reports | Microsoft Docs"
ms.date: 02/08/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: mobile-reports

ms.topic: conceptual
ms.assetid: 8adce9ad-6a08-4d20-b1cf-d3c45544d8de
author: markingmyname
ms.author: maghan
---
# Prepare data for Reporting Services mobile reports
  
[!INCLUDE[PRODUCT_NAME](../../includes/ss-mobilereptpub-long.md)] supports a number of complex data operations, including filtering, aggregation, and time slicing. This article offers some points to keep in mind while preparing data. Pre-aggregating data can optimize both mobile report creation and use, and some mobile report designs require it.   
  
## Date and time formats 
When dealing with date and time intervals for use in a mobile report, particularly with the TimeNavigator, it's important to format the date/time column properly so the [!INCLUDE[PRODUCT_NAME](../../includes/ss-mobilereptpub-short.md)] can identify it as such. Here are examples of valid date/time formats:  
  
    05/01/2009    
    2009-05-01    
    05/01/2009 14:57:32.8    
    2009-05-01 14:57:32.8    
    2009-05-01T14:57:32.8375298-04:00    
    5/01/2008 14:57:32.80 -07:00    
    1 May 2008 2:57:32.8 PM    
    Fri, 15 May 2009 20:10:57 GMT    
  
Date- and time-based datasets can, in most cases, be described by one or more date/time intervals, such as hourly, daily, monthly, quarterly, and yearly. [!INCLUDE[PRODUCT_NAME](../../includes/ss-mobilereptpub-short.md)] can combine multiple tables of different granularity and display them on a single mobile report. However, keep in mind the relevant intervals from the original dataset(s), as they can help when deciding what date/time filter options to present to the user in the final mobile report.  

Date fields in [!INCLUDE[ssASnoversion_md](../../includes/ssasnoversion-md.md)] multidimensional and tabular models can lose their date formatting in shared datasets. See [Retain date formatting for Analysis Services in mobile reports](../../reporting-services/mobile-reports/retain-date-formatting-for-analysis-services-in-mobile-reports.md) for a solution that keeps their formatting.
  
## Preparing filter data ##  
[!INCLUDE[PRODUCT_NAME](../../includes/ss-mobilereptpub-short.md)] can filter data based on both date/time fields and key fields. Though key fields can be numeric, in most cases they're either an ID or a string value. To prepare a filter field for use with a navigator element such as the Selection List, the filter key should be a single column in the data table. That way, you can group the table rows according to the value in the filter column. Having multiple columns contain different filter keys, or filter criteria, allows for mobile reports with multiple filter navigators to be used together hierarchically or individually.  
  
| Industry  | Country   | Region    |  
| ------------- | ------------- | ------------- |  
| Banks     | AFGHANISTAN   | ASIA      |  
| Commercial & Professional Services | AFGHANISTAN | ASIA |  
| Food, Beverage & Tobacco | AFGHANISTAN | ASIA |  
| Media | AFGHANISTAN | ASIA |  
| Pharmaceuticals | AFGHANISTAN | ASIA |  
| Food & Staples Retailing | ALBANIA | EUROPE |  
  
  
Key-based filters may also be hierarchically structured for use with a Selection List in a tree structure. To prepare data for use in this type of scenario, create a look-up table describing the hierarchical structure. Use a table structure that includes a Key column and a Parent Key column to indicate where a node belongs in the overall hierarchy.  
  
In this table, the ParentKey items are first listed in the ItemKey column, then listed in the ParentItemKey column next to their child items.   
  
|ItemKey    | ParentItemKey |  
| ------------- | ------------- |  
| Financials    |   |  
| Industrials   |   |  
| Consumer Staples |    |  
| Consumer Discretionary |  |     
| Health Care   |   |  
| Information Technology |  |  
| Banks | Financials |  
| Real Estate | Financials |  
| Diversified Financials |  Financials |   
| Insurance |   Financials |  
| Commercial & Professional Services |  Industrials |  
| Capital Goods |   Industrials |  
| Transportation |  Industrials |  
| Food, Beverage & Tobacco |    Consumer Staples |  
| Food & Staples Retailing |    Consumer Staples |  
| Household & Personal Products | Consumer Staples |  
| Media | Consumer Discretionary |  
| Automobiles and Components |  Consumer Discretionary |  
| Consumer Durables and Apparel |Consumer Discretionary |  
| Consumer Services |   Consumer Discretionary |  
| Retailing | Consumer Discretionary |  
| Pharmaceuticals   | Health Care |  
| Health Care Equipment & Services |    Health Care |  
| Software & Services | Information Technology |  
| Technology Hardware & Equipment   | Information Technology |  
| Telecommunication Services |Information Technology |  
  
### See also  
- [Prepare Excel data for Reporting Services mobile reports](../../reporting-services/mobile-reports/prepare-excel-data-for-reporting-services-mobile-reports.md)  
- [Retain date formatting for Analysis Services in mobile reports](../../reporting-services/mobile-reports/retain-date-formatting-for-analysis-services-in-mobile-reports.md)
- [Create mobile reports with SQL Server Mobile Report Publisher](../../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md)
  
  
  

