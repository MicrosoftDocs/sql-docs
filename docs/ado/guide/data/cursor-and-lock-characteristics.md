---
title: "Cursor and Lock Characteristics"
description: "Cursor and Lock Characteristics"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "locks [ADO], characteristics"
  - "adOpenDynamic [ADO]"
  - "cursors [ADO], characteristics"
---
# Cursor and Lock Characteristics
While the characteristics of a cursor depend upon capabilities of the provider, the following advantages and disadvantages generally apply to the various types of cursors and locks.  
  
|Cursor or lock type|Advantages|Disadvantages|  
|-------------------------|----------------|-------------------|  
|**adOpenForwardOnly**|-   Low resource requirements|-   Cannot scroll backward<br />-   No data concurrency|  
|**adOpenStatic**|-   Scrollable|-   No data concurrency|  
|**adOpenKeyset**|-   Some data concurrency<br />-   Scrollable|-   Higher resource requirements<br />-   Not available in disconnected scenario|  
|**adOpenDynamic**|-   High data concurrency<br />-   Scrollable|-   Highest resource requirements<br />-   Not available in disconnected scenario|  
|**adLockReadOnly**|-   Low resource requirements<br />-   Highly scalable|-   Data not updatable through cursor|  
|**adLockBatchOptimistic**|-   Batch updates<br />-   Allows disconnected scenarios<br />-   Other users able to access data|-   Data can be changed by multiple users at once|  
|**adLockPessimistic**|-   Data cannot be changed by other users while locked|-   Prevents other users from accessing data while locked|  
|**adLockOptimistic**|-   Other users able to access data|-   Data can be changed by multiple users at once|
