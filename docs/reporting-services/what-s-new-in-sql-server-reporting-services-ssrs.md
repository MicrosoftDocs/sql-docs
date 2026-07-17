---
title: "What's New in SQL Server Reporting Services (SSRS)"
description: Learn about what's new in the different versions of SQL Server Reporting Services (SSRS), including changes to the major feature areas.


ms.reviewer: randolphwest
ms.date: 06/16/2025
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: whats-new
ms.custom:
  - intro-whats-new
  - updatefrequency5
# customer intent:  As a SQL Server user, I want to stay updated with the latest features and enhancements in SQL Server Reporting Services (SSRS) so that I can leverage new capabilities, improve report performance, and ensure my reporting infrastructure is up-to-date and secure.
---

# What's new in SQL Server Reporting Services (SSRS)

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../includes/ssrs-appliesto-not-pbirs.md)]

Learn about what's new in the different versions of SQL Server [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. This article covers the major feature areas and is updated as new items are released.

::: moniker range="<sql-server-ver17"

For more information about SSRS, see [What is SQL Server Reporting Services (SSRS)?](create-deploy-and-manage-mobile-and-paginated-reports.md)

For information about Power BI Report Server, see [What's new in Power BI Report Server](/power-bi/report-server/whats-new).

::: moniker-end

::: moniker range="=sql-server-ver17"

## SQL Server 2025 Reporting Services changes

[!INCLUDE [ssrs-power-bi-consolidation](includes/ssrs-power-bi-consolidation.md)]

::: moniker-end

::: moniker range="=sql-server-ver16"

## SQL Server 2022 Reporting Services

Download [SQL Server 2022 Reporting Services](https://www.microsoft.com/download/details.aspx?id=104502) from the Microsoft Download Center.

This release introduces the new [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Reporting Services (SSRS). Innovation, creation, and design efforts focus on giving everyone the ability to achieve more. Designing for inclusivity reflects how people adapt to the world around them. In this new release of SSRS, significant accessibility improvements ensure broader empowerment and usability for users. The release includes:

- Windows Narrator support enhancements for the new Windows OS (Operating Systems) and Windows Server 
- Security enhancements
- Browser performance improvements with Angular
- Accessibility bug fixes
- Support for [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] instances report server catalog
- Reliability updates

### Updated web portal

The web portal was updated with a contemporary look.

:::image type="content" source="../reporting-services/media/report-server-2022-web-portal.png" alt-text="Screenshot of the updated [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Reporting Services web portal.":::

### Deprecated features

In 2020, deprecation of Report Server features [Pin to Power BI, Mobile Reports, and Mobile Report Publisher](deprecated-features-in-sql-server-reporting-services-ssrs.md) was announced. These features were removed from versions of SQL Server starting with [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and are no longer supported. SQL Server 2016, SQL Server 2017, and SQL Server 2019 are supported in maintenance mode until End of Service (EOS) for existing customers.

When a feature is deprecated, it's in maintenance mode only. There's no new feature development, including changes related to interoperability with new features. Deprecated features usually remain 
in future releases to make upgrades easier. However, in rare situations, the feature might be permanently removed from Reporting Services if it limits future innovations.

> [!NOTE]
> For new development work, don't use deprecated features.

::: moniker-end

::: moniker range="=sql-server-ver15"

## SQL Server 2019 Reporting Services

Download [SQL Server 2019 Reporting Services](https://www.microsoft.com/download/details.aspx?id=100122) from the Microsoft Download Center.

### Azure SQL Managed Instance support

You can now host a database catalog used for SSRS in an Azure SQL Managed Instance (MI) either on a virtual machine (VM) or in your data center. Support is limited to database credentials for the connection to SQL MI.

### Power BI Premium dataset support

You can connect to Power BI datasets by using either Microsoft Report Builder or SQL Server Data Tools (SSDT). Then you can publish those reports to SSRS 2019 by using SQL Server Analysis Services (SSAS) connectivity and use a stored Windows username and password to enable the scenario. For more information about SSAS, see - [What's new in SQL Server Analysis Services](/analysis-services/what-s-new-in-sql-server-analysis-services?viewFallbackFrom=sql-server-ver15)  


### AltText (alternative text) support for report elements

When you author reports, use tooltips to specify text for each element on the report. Screen reader technology identifies these tooltips properly.

<a name='azure-active-directory-application-proxy-support'></a>

### Microsoft Entra application proxy support

[!INCLUDE [entra-id](../includes/entra-id.md)]

With Microsoft Entra application proxy, you no longer need to manage your own web application proxy to allow secure access through the web or mobile apps.

### Custom headers

Sets header values for all URLs matching the specified regex pattern. You can update the custom header value with valid XML to set header values for selected request URLs. Administrators can add any number of headers in the XML. For more information, see [Custom headers](tools/server-properties-advanced-page-reporting-services.md#customheaders).

### Transparent data encryption

[!INCLUDE [sssql19-md](../includes/sssql19-md.md)] supports transparent data encryption (TDE) for the SSRS catalog database in the Enterprise and Standard editions.

### Microsoft Report Builder update

The newly released version of Report Builder is fully compatible with the 2016, 2017, and 2019 versions of Reporting Services. It's also compatible with all released and supported versions of Power BI Report Server.

::: moniker-end

::: moniker range="=sql-server-2017"

## SQL Server 2017 Reporting Services

Download [SQL Server 2017 Reporting Services](https://www.microsoft.com/download/details.aspx?id=55252) from the Microsoft Download Center.

### Comments on reports

Comments are available for reports. Comments can add perspective and help you collaborate with others. You can also include attachments with comments.

:::image type="content" source="media/what-s-new-in-sql-server-reporting-services-ssrs/report-server-comments.png" alt-text="Screenshot of the Comments button on a report.":::

For more information, see [Add comments to a report in a report server - Power BI Report Server](https://powerbi.microsoft.com/documentation/reportserver-add-comments/).

### REST API support

To enable development of modern applications and customization, SSRS supports a fully OpenAPI-compliant RESTful API. For information about the full API specification, see [SwaggerHub](https://app.swaggerhub.com/apis/microsoft-rs/SSRS/2.0).

### Query designer support for data analysis expressions (DAX)

In Report Builder and SSDT, you can create native DAX queries against supported SSAS tabular data models. Use the query designer in both tools to drag and drop the fields you want. The DAX query is then generated for you.

For more information, see [Reporting Services blog](/archive/blogs/sqlrsteamblog/query-designer-support-for-dax-now-available-in-report-builder-and-sql-server-data-tools).

* Download [SQL Server Report Builder](https://go.microsoft.com/fwlink/?LinkId=734968).
* Download [SQL Server Data Tools](../ssdt/download-sql-server-data-tools-ssdt.md).

> [!NOTE]
> You can only use the query designer for DAX with SSAS tabular data sources built in SQL Server 2016+.

### SharePoint integrated mode is deprecated

SharePoint integrated mode is deprecated after SQL Server 2016. To add Reporting Services reports to SharePoint, use the [Report Viewer web part on a SharePoint site - Reporting Services](../reporting-services/report-server-sharepoint/report-viewer-web-part-sharepoint-site.md).

::: moniker-end

## Related content

- [Reporting Services backward compatibility](reporting-services-backward-compatibility.md)
- [SQL Server Reporting Services features supported by editions](../reporting-services/reporting-services-features-supported-by-the-editions-of-sql-server.md)
- [Upgrade and migrate Reporting Services](../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md)
- [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
