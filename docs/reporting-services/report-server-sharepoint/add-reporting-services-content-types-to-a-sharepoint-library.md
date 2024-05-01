---
title: "Add Reporting Services content types to a SharePoint library"
description: Add a Report Builder Report, Report Model, and Report Data Source content type to a library to enable the New command to create new documents of that type.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2017
ms.service: reporting-services
ms.subservice: report-server-sharepoint
ms.topic: conceptual
ms.custom: updatefrequency5
monikerRange: ">=sql-server-2016 <=sql-server-2016"
---

# Add Reporting Services content types to a SharePoint library

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides predefined SharePoint content types that are used to manage shared data source (.rsds) files, report models (.smdl), and Report Builder report definition (.rdl) files. Add a **Report Builder Report**, **Report Model**, and **Report Data Source** content type to a library to enable the **New** command so that you can create new documents of that type.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

 To add content types to a library, you must be a site administrator or have Full Control level of permission.  
  
 The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] content types and content type management are automatically enabled in all document libraries for existing site collections created from the following site template types:  
  
-   **Business Intelligence Center**  
  
 Sites created after the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] integration don't have the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] content types enabled.  
  
> [!TIP]  
>  If you have **not** previously configured content types for a library, first enable management of content types, then enable the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] content types. See the procedures on enabling content type management in a single document library.  
  
 **Short video:** [(SSRS) Enabling Content Types in SharePoint2010.wmv](https://www.youtube.com/watch?v=yqhm3DrtT1w) (https://www.youtube.com/watch?v=yqhm3DrtT1w).  
  
 **In this topic:**  
  
-   [Enable content types in all document libraries in an existing BI center](#bkmk_enable_all)  
  
-   [Enable content type management for a single document library (SharePoint 2013)](#bkmk_enable_content_management)  
  
-   [Add Reporting Services content types (SharePoint 2013)](#bkmk_add_single)  
  
-   [Enable content type management for a single document library (SharePoint 2010)](#bkmk_enable_content_management_2010)  
  
-   [Add report server content types (SharePoint 2010)](#bkmk_add_single_2010)  
  
-   [Enable Content types and content management for multiple BI sites](#bkmk_enable_multiple_sites)  
  
##  <a name="bkmk_enable_all"></a> Enable content types in all document libraries in an existing BI center  
  
1.  To enable the content types and content management in all document libraries in an existing **Business Intelligence Center** site, you can toggle the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] integration feature.  
  
2.  Go to **Site settings**.  
  
    -   In SharePoint 2013, select the **Settings** icon.  
  
    -   In SharePoint 2010, select **Site Actions**, then select **Site Settings**.  
  
3.  Select **Site collection features**.  
  
4.  Find the **Report Server Integration Feature** and select **Deactivate**.  
  
     :::image type="content" source="../../reporting-services/report-server-sharepoint/media/rs-reportserver-integration-active.gif" alt-text="Screenshot of the Report Server Integration Feature showing the Deactivate and Activate options.":::  
  
5.  Refresh the browser then select **Activate** for the **Report Server Integration Feature**.  
  
     :::image type="content" source="../../reporting-services/report-server-sharepoint/media/rs-reportserver-integration-deactivate.gif" alt-text="Screenshot of the Report Server Integration Feature showing the Activate option.":::  
  
##  <a name="bkmk_enable_content_management"></a> Enable content type management for a single document library (SharePoint 2013)  
  
1.  Open the library for which you want to enable multiple content types.  
  
2.  Select **Library** in the ribbon.  
  
     :::image type="content" source="../../reporting-services/report-server-sharepoint/media/rs-sharepoint2013-libraryribbon.gif " alt-text="Screenshot that is showing the Library ribbon.":::
  
3.  On the **Library** ribbon, select **Library Settings**. If you don't see **Library Settings** or the button is disabled, you don't have permission to configure library settings, including content types.  
  
     :::image type="content" source="../../reporting-services/report-server-sharepoint/media/rs-sharepoint2013-librarysettings.gif " alt-text="Screenshot of the Library Settings button.":::
  
4.  In the **General Settings** section, select **Advanced settings**.  

     :::image type="content" source="../../reporting-services/report-server-sharepoint/media/rs-sharepoint2013-librarysettings-advancedsettings.gif " alt-text="Screenshot of the General Settings menu, highlighting Advanced settings.":::
  
5.  In the **Content Types** section, select **Yes** to allow management of content types.  
  
6.  Select **OK**.  
  
##  <a name="bkmk_add_single"></a> Add Reporting Services content types (SharePoint 2013)  
  
1.  Open the library for which you want to add Reporting Services content types.  
  
2.  On the ribbon, select **Library**.  
  
3.  Select **Library Settings**.  
  
4.  Under **Content Types**, select **Add from existing site content types**.  
  
5.  In **Select site content types from**, select **SQL Server Reporting Services Content Types**.  
  
6.  In the **Available Site Content Types** list, select **Report Builder**, and then choose **Add** to move the selected content type to the **Content types to add** list.  
  
7.  To add **Report Model** and **Report Data Source** content types, repeat the previous step.  
  
8.  When you finish adding content types, select **OK**.  
  
    > [!NOTE]  
    >  If the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] content type group **SQL Server Reporting Services Content Types** is not visible on the **Add Content Types** page, one of the following conditions is true:  
  
    -   The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint products isn't installed. For more information, see [Install or Uninstall the Reporting Services Add-in for SharePoint](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md). The article includes information on installing the add-in and stepping through a files only installation of the add-in to work around issues.  
  
    -   The add-in is installed but the site collection feature **Report Server Integration Feature** isn't active. Verify the site collection feature in **Site Settings**.  
  
    -   All of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] content types are in the library. If all the content types are part of a library, then the group is removed from the **Add Content Types** page. If you delete one or more of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] content types, then the group **SQL Server Reporting Services Content Types** are visible on the **Add Content Types** page.  
  
##  <a name="bkmk_enable_content_management_2010"></a> Enable content type management for a single document library (SharePoint 2010)  
  
1.  Open the library for which you want to enable multiple content types. On the library menu bar, you should see the following menus: **New**, **Upload**, **Actions**, and **Settings**. If you don't see **Settings**, you don't have permission to add a content type.  
  
2.  On the **Library Tools** ribbon, select **Library**.  
  
     :::image type="content" source="../../reporting-services/report-server-sharepoint/media/rs-sharepoint2010-libraryribbon.gif" alt-text="Screenshot that shows the Library Tools ribbon.":::
  
3.  On the **Settings** ribbon group, select **Library Settings**.  
  
4.  Under **General Settings**, select **Advanced settings**.  
  
5.  In the **Content Types** section, select **Yes** to allow management of content types.  
  
6.  Select **OK**.  
  
##  <a name="bkmk_add_single_2010"></a> Add report server content types (SharePoint 2010)  
  
1.  Open the library for which you want to add Reporting Services content types.  
  
2.  On the **Library Tools** ribbon tabs, select the **Library tab**.  
  
3.  On the **Settings** ribbon group, select **Library Settings**.  
  
4.  Under **Content Types**, select **Add from existing site content types**.  
  
5.  In the **Select Content Types** section, in **Select site content types from**, select the arrow to choose **SQL Server Reporting Services Content Types**.  
  
6.  In the **Available Site Content Types** list, select **Report Builder**, and then choose **Add** to move the selected content type to the **Content types to add** list.  
  
7.  To add **Report Model** and **Report Data Source** content types, repeat the previous step.  
  
8.  When you finish adding content types, select **OK**.  
  
##  <a name="bkmk_enable_multiple_sites"></a> Enable content types and content management for multiple BI sites  
  
1.  For SQL Server Reporting Services 2008 and 2008 R2 report servers, you can enable content types and content management for multiple Business Intelligence Center sites:  
  
2.  In SharePoint Central Administration, select **General Applications settings**. In the **SQL Server Reporting Services (2008 and 2008 R2)** section, select **Reporting Services Integration**.  
  
     :::image type="content" source="../../reporting-services/report-server-sharepoint/media/rs-general-app-settings.gif " alt-text="Screenshot of SQL Server Reporting Services.":::
  
3.  Select **Activate feature in all existing site collections**.  
  
     :::image type="content" source="../../reporting-services/report-server-sharepoint/media/rs-general-app-settings-old-integrations.gif" alt-text="Screenshot of the Activate the Reporting Services feature selection.":::

4.  Select **Ok**.  
  
## Related content  
 [SharePoint site and list permission reference for report server items](../../reporting-services/security/sharepoint-site-and-list-permission-reference-for-report-server-items.md)   
 [Start Report Builder](../../reporting-services/report-builder/start-report-builder.md)  
  
