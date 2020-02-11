---
title: "StartService Method (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "StartService Method (SqlService Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "StartService method"
ms.assetid: 83dfb6bd-dbd5-45d8-aad2-a11926317f91
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# StartService Method (SqlService Class)
  Attempts to place the service into its started state.  
  
## Syntax  
  
```  
  
object  
.StartService()  
  
```  
  
## Parts  
 *object*  
 A [SqlService Class](sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A uint32 value that specifies one of the following startup states.  
  
 0  
 Success. The request was accepted.  
  
 1  
 Not Supported. The request is not supported.  
  
 2  
 Access Denied. The user did not have appropriate access.  
  
 3  
 Dependent Services Running. The service cannot be stopped because other services that are running are dependent on it.  
  
 4  
 Invalid Service Control. The requested control code is not valid, or it is unacceptable to the service.  
  
 5  
 Service Cannot Accept Control. The requested control code cannot be sent to the service because the state of the service (Win32_BaseService:State) is equal to 0, 1, or 2.  
  
 6  
 Service Not Active. The service has not been started.  
  
 7  
 Service Request Timeout. The service did not respond to the start request in a timely fashion.  
  
 8  
 Unknown Failure. An unknown failure occurred when starting the service.  
  
 9  
 Path Not Found. The directory path to the service executable was not found.  
  
 10  
 Service Already Running. The service is already running.  
  
 11  
 Service Database Locked. The database to add a new service is locked.  
  
 12  
 Service Dependency Deleted. A dependency on which this service relies has been removed from the system.  
  
 13  
 Service Dependency Failure. The service failed to find the service needed from a dependent service.  
  
 14  
 Service Disabled. The service has been disabled from the system.  
  
 15  
 Service Logon Failed. The service does not have the correct authentication to run on the system.  
  
 16  
 Service Marked For Deletion. The service is being removed from the system.  
  
 17  
 Service No Thread. There is no execution thread for the service.  
  
 18  
 Status Circular Dependency. There are circular dependencies when starting the service.  
  
 19  
 Status Duplicate Name. There is a service running under the same name.  
  
 20  
 Status Invalid Name. There are characters that are not valid in the name of the service.  
  
 21  
 Status Invalid Parameter. Parameters that are not valid have been passed to the service.  
  
 22  
 Status Invalid Service Account. The account which this service is to run under is not valid, or it lacks the permissions to run the service.  
  
 23  
 Status Service Exists. The service exists in the database of services available from the system.  
  
 24  
 Service Already Paused. The service is currently paused in the system.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
