---
title: "Display page numbers or other paginated report properties | Microsoft Docs"
description:  Add properties of your paginated report including page numbers, filenames, and titles, for display in page headers or footers. 
ms.date: 01/18/2022
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: c7d95245-4709-4d04-acb4-59bf71e60d97
author: maggiesMSFT
ms.author: maggies
---
# Display page numbers or other paginated report properties (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  It's easy to add page numbers, a report title, file name, and other report properties to the page headers or footers of your paginated report. These properties are stored as fields in the Built-in Fields folder in the Report Data pane:  
  
-   Execution time    
-   Page number  
-   Report folder   
-   Report name   
-   Report server URL  
-   Total pages  
-   User ID   
-   Language  
  
 For a page number, you may want to add the word "Page" before the number. You may also want to show the total number of pages.  
  
> [!NOTE]  
> - Calculating the total number of pages may slow performance when you run or preview your report. We don't fully paginate the report unless you go to the end, as the tooltip states. The question mark (**?**) is by design, but you can force full pagination. One way to force it is to add a hidden text box in the footer or header with the expression "=Globals!OverallTotalPages". The report has to generate every page to answer that expression. 
> 
> - [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## To add a page number or other report properties  
  
1.  In the Report Data pane, expand the Built-in Fields folder.  
  
    If you don't see the Report Data pane, on the View tab, check **Report Data**.  
  
2.  Drag the **Page Number** field from the Report Data pane to the report header or footer.  

    A text box that contains the simple expression [&PageNumber] is added. 

    > [!NOTE]  
    >  The page footer is added to the report automatically. To add a page header, on the **Insert** tab, click **Header**, and then click **Add Header**.  
  
## To add the word "Page" before the page number  
  
1.  Right-click the text box that contains [&PageNumber] and click **Expressions**.  
  
     The **Set Expression for: Value** text box contains the expression =Globals!PageNumber.  
  
2.  Place the cursor after the = sign and type **"Page " &**.  
  
     The expression is now  ="Page "&Globals!PageNumber  
  
3.  Select **OK**.
  
## To add total number of pages after the page number  
  
1.  Right click the text box with the expression and click **Expressions**.  
  
2.  Type **&" of "&** at the end of the expression.  
  
3.  In the Category pane, expand **Built-in Fields** and double-click **TotalPages**.  
  
     The expression is now ="Page "&Globals!PageNumber &" of "&Globals!TotalPages  
  
4.  Select **OK**.


## See also  
 [Page Headers and Footers &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/page-headers-and-footers-report-builder-and-ssrs.md)   
 [Format Text in a Text Box &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/format-text-in-a-text-box-report-builder-and-ssrs.md)  
  
  
