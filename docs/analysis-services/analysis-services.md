---
title: "About SQL Server Analysis Services | Microsoft Docs"
ms.date: 12/05/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: overview
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# About SQL Server Analysis Services

Analysis Services is an analytical data engine used in decision support and business analytics. It provides enterprise-grade semantic data models for business reports and client applications such as Power BI, Excel, Reporting Services reports, and other data visualization tools.

A typical workflow includes creating a tabular or multidimensional data model project in Visual Studio, deploying the model as a database to a server instance, setting up recurring data processing, and assigning permissions to allow data access by end-users. When it's ready to go, your semantic data model can be accessed by client applications supporting Analysis Services as a data source.

Analysis Services is available in two different platforms:

**Azure Analysis Services** - Supports tabular models at the 1200 and higher compatibility levels. DirectQuery, partitions, row-level security, bi-directional relationships, and translations are all supported. To learn more, see [Azure Analysis Services](https://docs.microsoft.com/azure/analysis-services/).

**SQL Server Analysis Services** - Supports tabular models at all compatibility levels, multidimensional models, data mining, and Power Pivot for SharePoint.

## Documentation by area

In general, [Azure Analysis Services documentation](https://docs.microsoft.com/azure/analysis-services/) is included with Azure documentation. If you're interested in having your tabular models in the cloud, it's best to start there. This article and documentation in this section is mostly for SQL Server Analysis Services. However, at least for tabular models, how you create and deploy your tabular model projects is much the same, regardless of the platform you're using. Check out these sections to learn more:

- [Comparing Tabular and Multidimensional Solutions](../analysis-services/comparing-tabular-and-multidimensional-solutions-ssas.md)
- [Install SQL Server Analysis Services](../analysis-services/instances/install-windows/install-analysis-services.md)
- [Tabular models](../analysis-services/tabular-models/tabular-models-ssas.md)
- [Multidimensional models](../analysis-services/multidimensional-models/multidimensional-models-ssas.md)
- [Data Mining](../analysis-services/data-mining/data-mining-ssas.md)
- [Power Pivot for SharePoint](../analysis-services/power-pivot-sharepoint/power-pivot-for-sharepoint-ssas.md)
- [Tutorials](../analysis-services/analysis-services-tutorials-ssas.md)
- [Server management](../analysis-services/instances/analysis-services-instance-management.md)
- [Developer documentation](analysis-services-developer-documentation.md)
- [Technical reference](https://docs.microsoft.com/bi-reference/)

#### See also

[Azure Analysis Services documentation](https://docs.microsoft.com/azure/analysis-services/)
[SQL Server Documentation](../sql-server/sql-server-technical-documentation.md)
