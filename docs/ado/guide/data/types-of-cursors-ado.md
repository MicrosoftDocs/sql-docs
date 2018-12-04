---
title: "Types of Cursors (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "cursors [ADO], types"
ms.assetid: 7cc01544-e814-403b-bbfe-a2750bf921bd
author: MightyPen
ms.author: genemi
manager: craigg
---
# Types of Cursors (ADO)
As a general rule, your application should use the simplest cursor that provides the required data access. Each additional cursor characteristic beyond the basics (forward-only, read-only, static, scrolling, unbuffered) has a price - in client memory, network load, or performance. In many cases, the default cursor options generate a more complex cursor than your application actually needs.  
  
 Your choice of cursor type depends on how your application uses the result set and also on several design considerations, including the size of the result set, the percentage of the data likely to be used, sensitivity to data changes, and application performance requirements.  
  
 At its most basic, your cursor choice depends on whether you need to change or simply view the data:  
  
-   If you just need to scroll through a set of results, but not change data, use a [forward-only](../../../ado/guide/data/forward-only-cursors.md) or [static](../../../ado/guide/data/static-cursors.md) cursor.  
  
-   If you have a large result set and need to select just a few rows, use a [keyset](../../../ado/guide/data/keyset-cursors.md) cursor.  
  
-   If you want to synchronize a result set with recent adds, changes, and deletes by all concurrent users, use a [dynamic](../../../ado/guide/data/dynamic-cursors.md) cursor.  
  
 Although each cursor type seems to be distinct, keep in mind that these cursor types are not so much different varieties as simply the result of overlapping characteristics and options.  
  
 This section contains the following topics.  
  
-   [Forward-Only Cursors](../../../ado/guide/data/forward-only-cursors.md)  
  
-   [Static Cursors](../../../ado/guide/data/static-cursors.md)  
  
-   [Keyset Cursors](../../../ado/guide/data/keyset-cursors.md)  
  
-   [Dynamic Cursors](../../../ado/guide/data/dynamic-cursors.md)  
  
## See Also  
 [Forward-Only Cursors](../../../ado/guide/data/forward-only-cursors.md)   
 [Static Cursors](../../../ado/guide/data/static-cursors.md)   
 [Keyset Cursors](../../../ado/guide/data/keyset-cursors.md)   
 [Dynamic Cursors](../../../ado/guide/data/dynamic-cursors.md)
