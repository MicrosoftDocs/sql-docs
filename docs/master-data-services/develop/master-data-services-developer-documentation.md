---
title: "Master Data Services Developer Documentation | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "mds"
ms.reviewer: ""
ms.technology: 

ms.topic: "reference"
ms.assetid: 067b1f69-84eb-4a13-b220-120cd63704b4
author: leolimsft
ms.author: lle
manager: craigg
---
# Master Data Services Developer Documentation

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  Find information about how to write code to customize the way you and your users interact with [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]. Learn how to:  
  
-   Write a program that accesses the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web service. The [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web service is a Windows Communication Foundation (WCF) service that developers use to control [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] features through code.  
  
-   Incorporate [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] features into existing applications.  
  
-   Write code to perform repetitive or complex actions that are difficult or impossible to do with the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] UI.  
  
-   Create a custom workflow that executes in response to a business rule you specify. A custom workflow calls code that you write, which can take whatever action you require to process the workflow.  
  
## Master Data Manager Web Service  
 The [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web service lets you make programmatic use of the features of [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] from any computer that can access your [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web site. Before you can start writing code to access the web service, you must generate proxy classes, which are contained in a namespace that you specify. This documentation uses <xref:Microsoft.MasterDataServices> as the proxy namespace. The main proxy class you use to perform web service operations is the <xref:Microsoft.MasterDataServices.ServiceClient> class, which implements the <xref:Microsoft.MasterDataServices.IService> interface. From your code, call methods of the <xref:Microsoft.MasterDataServices.ServiceClient> class to access the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web service. The remainder of the classes in the namespace are used by the web service operations.  
  
### Web Service Content  
 [Create Master Data Manager Web Service Proxy Classes](../../master-data-services/develop/create-master-data-manager-web-service-proxy-classes.md)  
 Describes how to enable metadata publishing from the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web site and how to create proxy classes that can be used to programmatically access the web service operations.  
  
 [Categorized Web Service Operations &#40;Master Data Services&#41;](../../master-data-services/develop/categorized-web-service-operations-master-data-services.md)  
 A categorized list of the web service operations of the <xref:Microsoft.MasterDataServices.ServiceClient> class.  
  
## Custom Workflows  
 [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] uses business rules to create basic workflow solutions. You can automatically update and validate data and have e-mail notifications sent based on conditions you specify. Business rules in [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] are intended to manage the most common workflow scenarios. If your workflow requires more complex event processing, such as multi-tiered approvals or complex decision trees, you can configure [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] to send data to a custom assembly that you create. To handle custom workflows, you must configure and start SQL Server MDS Workflow Integration Service on the web application computer, and create an assembly that implements the <xref:Microsoft.MasterDataServices.WorkflowTypeExtender.IWorkflowTypeExtender> interface.  
  
### Custom Workflow Content  
 [Create a Custom Workflow &#40;Master Data Services&#41;](../../master-data-services/develop/create-a-custom-workflow-master-data-services.md)  
 Instructions on how to create a workflow handler assembly, how to configure and start SQL Server MDS Workflow Integration Service, and how to create a business rule in [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] that starts a custom workflow.  
  
## Web Server Namespaces  
 [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] installs a set of assemblies on the web server computer. These assemblies contain namespaces that can be used for advanced scenarios that customize the behavior of the web server computer. The following table describes these namespaces.  
  
|Namespace|Description|  
|---------------|-----------------|  
|<xref:Microsoft.MasterDataServices.Deployment>|Contains classes that can be used to create a deployment package from a model and to deploy a package into a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database.|  
|<xref:Microsoft.MasterDataServices.Services>|Contains a class that receives and processes web service operations made to the web server computer through the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application.|  
|<xref:Microsoft.MasterDataServices.Services.DataContracts>|Contains classes that define how data is passed from the client computer through the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application to the web server computer.|  
|<xref:Microsoft.MasterDataServices.Services.MessageContracts>|Contains classes that define how requests and responses are passed from the client computer through the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application to the web server computer.|  
|<xref:Microsoft.MasterDataServices.Services.ServiceContracts>|Contains the interface that defines the operations that can be called through the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web service.|  
  
  
