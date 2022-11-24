---
title: "Service Providers and Components"
description: "Service Providers and Components"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "OLE DB providers [ADO]"
  - "ADO, OLE DB providers"
  - "service providers [ADO]"
---
# Service Providers and Components
Service providers are components that extend the functionality of data providers by implementing extended interfaces that are not natively supported by the data store.  
  
 Universal Data Access provides a *component architecture* that allows individual, specialized components to implement discrete sets of database functionality, or "services," on top of less capable stores. Thus, rather than forcing each data store to provide its own implementation of extended functionality or forcing generic applications to implement database functionality internally, service components provide a common implementation that any application can use when accessing any data store. The fact that some functionality is implemented natively by the data store and some through generic components is transparent to the application.  
  
 For example, a cursor engine, such as [The Cursor Service for OLE DB](/previous-versions/windows/desktop/ms714397(v=vs.85)), is a service component that can consume data from a sequential, forward-only data store to produce scrollable data. Other service providers commonly used by ADO include the [Microsoft OLE DB Persistence Provider (ADO Service Provider)](../../../ado/guide/appendixes/microsoft-ole-db-persistence-provider-ado-service-provider.md) (for saving data to a file), the [Microsoft Data Shaping Service for OLE DB (ADO Service Provider)](../../../ado/guide/appendixes/microsoft-data-shaping-service-for-ole-db-ado-service-provider.md) (for hierarchical **Recordsets**), and the [Microsoft OLE DB Remoting Provider (ADO Service Provider)](../../../ado/guide/appendixes/microsoft-ole-db-remoting-provider-ado-service-provider.md) (for invoking data providers on a remote computer).  
  
 For more information about service and data providers, see [Appendix A: Providers](../../../ado/guide/appendixes/appendix-a-providers.md).