---
title: "Import Cleansing Project Values into a Domain | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dqs.kb.importprojectvalues.f1"
ms.assetid: f23e38e2-39e0-42d7-abd5-34d8fcca5d2a
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Import Cleansing Project Values into a Domain

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  In [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS), you can import data quality knowledge gathered during the cleansing process in a data quality cleansing project or an Integration Services package containing the DQS Cleansing component into a domain. This ensures that trusted knowledge is not lost, and that the knowledge base is continually improved.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   To import cleansing project values into a domain, the domain must have been used in the cleansing project in Data Quality Client or in the Integration Services package containing a DQS Cleansing component.  
  
-   The cleansing project in Data Quality Client or the Integration Services package containing the DQS Cleansing component must have successfully completed.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 You must have the dqs_kb_editor or the dqs_administrator role on the DQS_MAIN database to import data quality knowledge gathered during the cleansing process into a domain.  
  
##  <a name="Import"></a> Import Cleansing Project Values  
  
1.  [!INCLUDE[ssDQSInitialStep](../includes/ssdqsinitialstep-md.md)] [Run the Data Quality Client Application](../data-quality-services/run-the-data-quality-client-application.md).  
  
2.  In the [!INCLUDE[ssDQSClient](../includes/ssdqsclient-md.md)] home screen, open a knowledge base in the Domain Management activity.  
  
3.  If adding values to an existing domain, select the domain in the domain list.  
  
4.  Click the **Domain Values** tab, click the **Import Values** icon in the icon bar, and then click **Import project values**. The **Import Project Values** dialog box appears with a list of data quality projects and Integration Services packages that were cleansed using the domain.  
  
    > [!NOTE]  
    >  If no project has been created using the domain or any of its linked domains, or the project was not finished, the **Import project values** option will not be available.  
  
5.  In the **Import Project Values** dialog box:  
  
    -   Select **All** in the **Imported** drop-down list to display all projects, or **No** to display only projects whose values have not been imported yet.  
  
    -   Select the project that you want to import values from.  
  
    -   Select **Add values from New tab** to import values in the new tab, in addition to values in the **Correct** and **Corrected** tabs.  
  
    -   Click **OK**.  
  
6.  You return to the **Domain Values** tab, and a message is displayed on successful import of the values. Values that have been imported, and so are new to the domain, will be displayed in the **Values** table.  
  
7.  Deselect **Show Only New** to display all values that are in the domain.  
  
8.  Select **Correct**, **Error**, or **Invalid** to display only those values of the selected type.  
  
9. To search for a specific string, enter the string in the **Find** text box. Click the up or down arrow to step through the values that meet the search criteria. They will be highlighted in yellow.  
  
10. Click **Finish**.  
  
    > [!NOTE]  
    >  For more information on working with values in the **Domain Values** tab, see [Change Domain Values](../data-quality-services/change-domain-values.md).  
  
##  <a name="FollowUp"></a> Follow Up: After Importing Project Values into a Domain  
 After you import data quality knowledge gathered during the cleansing process into a domain, you can perform other domain management tasks on the domain and the values. For more information, see [Managing a Domain](../data-quality-services/managing-a-domain.md).  
  
##  <a name="Values"></a> Values that Will Be Imported  
 The following values will be imported from a project into a domain:  
  
-   Only string values are imported to the domain.  
  
-   Only values from the **Correct**, **Corrected**, and **New** tabs on the **Manage and View results** page of the **Cleansing** activity will be imported. Values from the **New** tab will be imported only if the **Add values from New tab** check box in the **Import Project Values** dialog box has been selected.  
  
-   Values will be imported as correct or as an error with its correction. Only an error value with a correction value will be imported.  
  
-   The correction value will be either a new value that does not exist in the knowledge base or an existing correct value.  
  
-   Only corrections performed on the value level, not the record level, will be imported into the knowledge base.  
  
-   Invalid values will be created if the imported value contradicts a domain rule.  
  
-   If you import values from several projects at once, the values are imported in a sequential order.  
  
-   A correction made as a result of a term-based relation in a domain is imported as a correct value (not as an error).  
  
##  <a name="ValuesNot"></a> Values that Will Not Be Imported  
 The following values will not be imported from a project into a domain:  
  
-   Values from the **Suggested** and **Invalid** tabs on the **Manage and View results** page of the **Cleansing** activity will not be imported.  
  
-   If a value found in the cleansing project contradicts an existing value in the domain, the value found in the project is skipped. This will include conflicts between cleansing and knowledge base values.  
  
-   Corrections performed on the record level will not be imported into the knowledge base.  
  
-   No value will be imported to a domain if the value that it would replace was corrected or approved as correct by a reference data service.  
  
-   If a correction value appears in the knowledge base as an invalid or error value, neither the error nor the correction value will be imported.  
  
-   If the domain is part of a composite domain, and the cleansing was performed on the composite domain, no values will be imported.  
  
-   You can import values from a project only when the knowledge base has a state of in-work and the knowledge base is locked by the user who is importing.  
  
## See Also  
 [Data Cleansing](../data-quality-services/data-cleansing.md)   
 [DQS Cleansing Transformation](../integration-services/data-flow/transformations/dqs-cleansing-transformation.md)  
  
  
