---
title: "Command-Line Management Tool: SqlLocalDB.exe | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 

ms.topic: "reference"
apilocation: 
  - "sqluserinstance.dll"
ms.assetid: dd0882b1-a8a9-447a-8bdf-0f9d7f36d336
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# Command-Line Management Tool: SqlLocalDB.exe
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  SqlLocalDB.exe is a simple tool that enables the user to easily manage LocalDB instances from the command line. It is implemented as a simple wrapper around the LocalDB instance API. As in many similar SQL Server tools (for example, SQLCMD), parameters are passed to SqlLocalDB as command-line arguments and output is sent to the console.  
  
 SqlLocalDB enables developers to use LocalDB without having to write code to call the API or depend on other tools to do it for them.  
  
## SqlLocalDB Options  
 SqlLocalDB supports the following options.  
  
|Option|What it does|  
|------------|------------------|  
|`-?`|Prints help text.|  
|`create\|c "instance name" [version-number] [-s]`|Creates a new LocalDB instance with a specified name and version.<br /><br /> If the [version-number] parameter is omitted, it defaults to the SqlLocalDB build version.<br /><br /> -s starts the new LocalDB instance after it is created.|  
|`delete\|d "instance name"`|Deletes the LocalDB instance with the specified name.|  
|`start\|s "instance name"`|Starts the LocalDB instance with the specified name.|  
|`stop\|p "instance name" [-i\|-k]`|Stops the LocalDB instance with the specified name, after current queries finish running.<br /><br /> -i requests the LocalDB instance shutdown with the NOWAIT option.<br /><br /> -k kills the LocalDB instance process without contacting it.|  
|`share\|h ["owner SID or account"] "private name" "shared name"`|Shares the specified private instance using the specified shared name. If the user SID or account name is omitted, it defaults to the current user.|  
|`unshare\|u "shared name"`|Unshares the specified shared LocalDB instance.|  
|`info\|i`|Lists all existing LocalDB instances that are owned by the current user and all shared LocalDB instances.|  
|`info\|i "instance name"`|Prints the information about the specified LocalDB instance.|  
|`versions\|v`|Lists all LocalDB versions installed on the computer.|  
|||  
|`trace\|t on\|off`|Turns tracing on and off.|  
  
 SqlLocalDB treats spaces as delimiters; you must surround the instance names that contain spaces and special characters with quotes. For example:  
  
 `SqlLocalDB create "My instance name with spaces"`  
  
  
