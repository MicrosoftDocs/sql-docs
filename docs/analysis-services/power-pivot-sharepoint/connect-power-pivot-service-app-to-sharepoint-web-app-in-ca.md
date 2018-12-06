---
title: "Connect Power Pivot Service App to SharePoint Web App in CA | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: ppvt-sharepoint
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Connect Power Pivot Service App to SharePoint Web App in CA
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application can be used by any number of SharePoint Web applications in the farm. To make a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application available, you add it to a service association list.  
  
> [!IMPORTANT]  
>  You must have one [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application in the default group to ensure that [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard works properly. Do not add more than one [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application to the default group. Adding multiple entries of the same service application type is not a supported configuration and might cause errors. If you are creating additional service applications, add them to custom lists.  
  
 This topic contains the following sections:  
  
 [Add Power Pivot Services Application to the Default Group](#default)  
  
 [Add Power Pivot Services Application a Custom Service Association List](#custom)  
  
##  <a name="default"></a> Add Services Application to the Default Group  
 A service association list is a list of shared services that provide resources to other SharePoint Web applications in the farm. There is one default group of service associations for the farm.  
  
 To be on the list, a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application can either be added when you create the application or afterwards by using the following steps.  
  
1.  In Central Administration, in **Application Management**, click **Configure service application associations**.  
  
2.  In Application Proxy Group, click **default**.  
  
3.  Select the checkbox next to the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application (indicated by type name **Power Pivot Service Application Proxy**). If you have more than one [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application, choose just one.  
  
4.  Click **OK**.  
  
##  <a name="custom"></a> Add [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Services Application a Custom Service Association List  
 The default group can be replaced by a custom list. A custom list is created specifically for a single SharePoint Web application. It overrides the default group and replaces it with only those service associations that a farm or service administrator specifies. If you created multiple [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service applications, you must use a custom list to specify which one to use. A custom list cannot be reused by other Web applications. It applies only to the Web application for which it was created.  
  
1.  In Central Administration, in **Application Management**, click **Manage web applications**.  
  
2.  Select the application (for example, SharePoint -80).  
  
3.  In Web Applications, in Manage, click **Service Connections**.  
  
4.  In **Edit the following group of connections**, select **[custom]**.  
  
5.  Select the checkbox next to each service application connection you want to use. If you have multiple [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service applications (indicated by Type set to **Power Pivot Service Application Proxy**), be sure to choose only one.  
  
6.  Click **OK**.  
  
## See Also  
 [Create and Configure a Power Pivot Service Application in Central Administration](../../analysis-services/power-pivot-sharepoint/create-and-configure-power-pivot-service-application-in-ca.md)   
 [Initial Configuration (Power Pivot for SharePoint)](http://msdn.microsoft.com/3a0ec2eb-017a-40db-b8d4-8aa8f4cdc146)  
  
  
