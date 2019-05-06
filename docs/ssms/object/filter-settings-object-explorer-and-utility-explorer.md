---
title: "Filter Settings (Object Explorer and Utility Explorer) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.common.filtersettings.f1"
  - "sql13.ag.job.filtersettings.f1"
ms.assetid: 4aab04bc-e1ab-4d4b-ab74-b287fc805bc2
author: "markingmyname"
ms.author: "maghan"
manager: craigg
---
# Filter Settings (Object Explorer and Utility Explorer)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Use this dialog box to specify a filter. A filter allows you to configure Object Explorer and Utility Explorer to display only items that meet specific criteria. For example, you can use a filter to show only jobs with names that contain the word "Maintenance." The header for the **Filter Settings** dialog box contains the name of the server, and it may contain the name of the database.  
  
## UIElement List  
**Property**  
Displays the property to filter on.  
  
**Operator**  
Select the way that the filter applies the value to the property. There are the following options:  
  
-   **Equals**  
  
    The filter shows items where the property and the value are an exact match.  
  
-   **Contains**  
  
    The filter shows items where the property contains the value. The property may contain other text.  
  
-   **Does not contain**  
  
    The filter shows items that where the property does not contain the value.  
  
-   **Less than**  
  
    Available for dates, this filter shows items whose date is before the value provided.  
  
-   **Less than or equal**  
  
    Available for dates, this filter shows items whose date contains or is before the value provided.  
  
-   **Greater than**  
  
    Available for dates, this filter shows items whose date is after the value provided.  
  
-   **Greater than or equal**  
  
    Available for dates, this filter shows items whose date contains or is after the value provided.  
  
-   **Between**  
  
    Available for dates, this filter shows items whose date is between two dates provided. Select **Between** and press TAB to add another row allowing entry of the second date.  
  
-   **Not between**  
  
    Available for dates, this filter shows items whose date is before or after two dates provided. Select **Not Between** and tab out of the **Operator** column to add another row allowing entry of the second date.  
  
**Value**  
Type the value to compare to the property. For dates, click the down arrow to show a calendar for selecting the date.  
  
**Clear Filter**  
Removes all current filter settings.  
  
## See Also  
[Use SQL Server Management Studio](../../ssms/use-sql-server-management-studio.md)  
[Overview of SQL Server Utility](../../relational-databases/manage/sql-server-utility-features-and-tasks.md)  
  
