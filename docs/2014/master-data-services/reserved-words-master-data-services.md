---
title: "Reserved Words (Master Data Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
helpviewer_keywords: 
  - "reserved words [Master Data Services]"
  - "Master Data Services, reserved words"
ms.assetid: 88afd0d0-4362-4394-8357-4e65388fc0fc
author: leolimsft
ms.author: lle
manager: craigg
---
# Reserved Words (Master Data Services)
  In [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], when you create model objects or members, some words cannot be used. Using these words may cause errors.  
  
> [!NOTE]  
>  You should also limit your use of special characters (symbols, hyphenation, etc.).  
  
-   [Models](#models)  
  
-   [Entities](#entities)  
  
-   [Explicit Hierarchies](#exhierarchies)  
  
-   [Attributes](#attributes)  
  
-   [Members](#members)  
  
##  <a name="models"></a> Models  
 If you create a model with the name set to **Name**, do not select **Create entity with same name as model** because **Name** cannot be used for the name of an entity.  
  
##  <a name="entities"></a> Entities  
 For entity names, you cannot use **Name** or **Code**.  
  
##  <a name="exhierarchies"></a> Explicit Hierarchies  
 For explicit hierarchy names, you cannot use **Name** or **Code**.  
  
##  <a name="attributes"></a> Attributes  
  
-   **ID**  
  
-   **Code**  
  
-   **Name**  
  
-   **EnterDTM**  
  
-   **EnterUserID**  
  
-   **EnterUserName**  
  
-   **LastChgDTM**  
  
-   **LastChgUserID**  
  
-   **Status_ID**  
  
-   **ValidationStatus_ID**  
  
-   **Version_ID**  
  
##  <a name="members"></a> Members  
 For members, you cannot use **MDMMemberStatus** or **ROOT** for the **Code** attribute value.  
  
## See Also  
 [Master Data Services Overview](master-data-services-overview-mds.md)  
  
  
