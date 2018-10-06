---
title: "Network Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "LingerTimeout property"
  - "EnableNagleAlgorithm property"
  - "MinPendingAcceptExCount property"
  - "MaxPendingSendCount property"
  - "EnableBinaryXML property"
  - "MinPendingReceiveCount property"
  - "MaxCompletedReceiveCount property"
  - "DisableNonblockingMode property"
  - "RequestSizeThreshold property"
  - "CompressionLevel property"
  - "ReceiveBufferSize property"
  - "EnableCompression property"
  - "ServerSendTimeout property"
  - "IPV4Support property"
  - "MaxPendingReceiveCount property"
  - "MaxPendingAcceptExCount property"
  - "IPV6Support property"
  - "MaxAllowedRequestSize property"
  - "ServerReceiveTimeout property"
  - "EnableLingerOnClose property"
  - "InitialConnectTimeout property"
  - "SendBufferSize property"
  - "ScatterReceiveMultiplier property"
  - "network properties [Analysis Services]"
ms.assetid: ef4251e2-abe5-4c5b-9868-7549782d0244
author: minewiskan
ms.author: owend
manager: craigg
---
# Network Properties
  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports the server properties listed in the following tables. For more information about additional server properties and how to set them, see [Configure Server Properties in Analysis Services](server-properties-in-analysis-services.md).  
  
 **Applies to:** Multidimensional and Tabular server mode  
  
## General  
 `ListenOnlyOnLocalConnections`  
 A Boolean property that identifies whether to listen only on local connections, for example localhost.  
  
## Listener  
 `IPV4Support`  
 A signed 32-bit integer property that defines support for IPv4 protocol. This property has one of the values listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|*0*|IPv4 disabled; clients can't connect.|  
|*1*|(Default) IPv4 is required; server won't start if it cannot listen to IPv4.|  
|*2*|IPv4 is optional; server tries to listen to IPv4 but starts even if unable to.|  
  
 `IPV6Support`  
 A signed 32-bit integer property that defines support for IPv6 protocol. This property has one of the values listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|*0*|IPv6 disabled; clients can't connect.|  
|*1*|(Default) IPv6 is required; server won't start if it cannot listen to IPv6.|  
|*2*|IPv6 is optional; server tries to listen to IPv6 but starts even if unable to.|  
  
 `MaxAllowedRequestSize`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `RequestSizeThreshold`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `ServerReceiveTimeout`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `ServerSendTimeout`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
## Requests  
 `EnableBinaryXML`  
 A Boolean property that specifies whether the server will recognize requests binary xml format.  
  
 `EnableCompression`  
 A Boolean property that specifies whether compression is enabled for requests.  
  
## Responses  
 `CompressionLevel`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `EnableBinaryXML`  
 A Boolean property that specifies whether the server is enabled for binary xml responses.  
  
 `EnableCompression`  
 A Boolean property that specifies whether compression is enabled for responses to client requests.  
  
## TCP  
 `InitialConnectTimeout`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `MaxCompletedReceiveCount`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `MaxPendingAcceptExCount`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `MaxPendingReceiveCount`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `MaxPendingSendCount`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `MinPendingAcceptExCount`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `MinPendingReceiveCount`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `ScatterReceiveMultiplier`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `SocketOptions\ DisableNonblockingMode`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `SocketOptions\ EnableLingerOnClose`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `SocketOptions\EnableNagleAlgorithm`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `SocketOptions\ LingerTimeout`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `SocketOptions\ ReceiveBufferSize`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `SocketOptions\ SendBufferSize`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
## See Also  
 [Configure Server Properties in Analysis Services](server-properties-in-analysis-services.md)   
 [Determine the Server Mode of an Analysis Services Instance](../instances/determine-the-server-mode-of-an-analysis-services-instance.md)  
  
  
