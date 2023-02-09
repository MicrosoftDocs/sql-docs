---
title: Troubleshooting Database Migration Assessment for Oracle extension errors
description: Learn how to install the Azure Data Studio Database Migration Assessment for Oracle extension. This extension helps guide you choose the best Azure SQL database and Azure Database for PostgreSQL migration path.
author: abhims14
ms.author: abhims14
ms.reviewer: 
ms.date: 09/02/23
ms.service: azure-data-studio
ms.topic: conceptual
---

 
# Troubleshooting Database Migration Assessment for Oracle extension errors

This article explains how to determine, diagnose, and fix issues/errors that you might encounter when you use  Database Migration Assessment for Oracle extension.



## Troubleshoot

To troubleshoot any Database Migration Assessment for Oracle extension issue, you may have to find out the details  about the error, and warnings from the logs generated. Refer this section to access the logs.



### Logs

The extension stores errors, warnings, and other diagnostic logs in the default log directory:

- Windows - `C:\Users\<username>.dmaoracle\logs\`
- Linux - `~/.dmaoracle/logs`
- macOS - `/Users/<username>/.dmaoracle/logs`

> [!NOTE]
> By default, the extension stores the last seven log files.
To change the log directory, update the `LogDirectory` property in the extension settings file.

|Operating system|Path|
|---|---|
|Windows|`C:\Users\<username>\.azuredatastudio\extensions\microsoft.azuredatastudio-dma-oracle-<VersionNumber>\bin\service\Properties\ConfigSettings\extension-settings.json`|
|Linux|`~/.azuredatastudio/extensions/microsoft.azuredatastudio-dma-oracle-/<VersionNumber>/bin/service/Properties/ConfigSettings/extension-settings.json`|
|macOS|`/Users/<username>/.azuredatastudio/extensions/microsoft.azuredatastudio-dma-oracle-/<VersionNumber>/bin/service/Properties/ConfigSettings/extension-settings.json`|


## Error Codes Details

Below section gives you more details about error codes, messages, possible causes and their respective remediation actions.



### Error Code: FSMB-0001

Error Message: Failed to access assessment data.

Possible causes: This error may appear due to any of the given reasons-
1.	The user may not have the appropriate permission to access the assessment data folder.
2.	The assessment data folder doesn't exist or is in use by another program.
 
Remediation actions: You need to ensure that the logged in user has read and write permissions on the assessment data folder. In case you've changed the assessment data folder path, check the current assessment path mentioned in extension settings is correct. 


 
### Error Code: GE-0000
 
Error Message: The data collection failed with an unknown error. Try creating a new assessment.
 
Possible Causes: This error is generic and may appear due to multiple reasons.
 
Remediation actions: Try to create a new assessment. To investigate further, go through the [logs](https://aka.ms/logsDMAO) to understand the reason of error. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO). 
 


### Error Code: FSMB-0002
 
Error Message: Failed to parse the assessment data. A metadata file is either corrupted or modified externally. Try creating a new assessment.
 
Possible Causes: This error may appear due to any of the given reasons-
1.	The metadata file is either corrupted or modified externally.
2.	Couldn't parse the json for the metadata file correctly.
 
Remediation actions: You may have to delete the current assessment and then try creating new assessment. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO).


 
### Error Code: FSMB-0003
 
Error Message: Failed to save the assessment output with a file system error.
 
Possible Causes: This error may appear due to any of the given reasons-
1.	The user may not have the appropriate permission to the assessment data folder.
2.	The assessment data folder is already in use by another program.
 
Remediation actions: You need to ensure that the logged in user has read and write permissions to the assessment data folder. In case you've changed the assessment data folder path, check the current assessment path mentioned in extension settings is correct and exists. Try to create a new assessment after closing any open assessment file(s).


 
### Error Code: ODC-1000
 
Error Message: Failed to collect required data with an underlying Oracle exception - {ORA error}. Check Oracle documentation for this error code.
 
Possible Causes: Assessment failed due to an underlying Oracle error - {ORA error}.
 
Remediation actions: You may need to refer Oracle documentation to resolve this issue. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO).


 
### Error Code: FSRP-0001
 
Error Message: A terminal error occurred - failed to access lookup data. A lookup file is either deleted or corrupted. Reinstall the extension and try again.
 
Possible Causes: This error may appear due to any of the given reasons-
1)	The lookup metadata file is either deleted or corrupted. 
2)	The lookup metadata file is already in use by another program.
 
Remediation actions: Try to reinstall the extension, close any open assessment file(s), and then create a new assessment. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO).


 
### Error Code: GE-2000
 
Error Message: Failed to process the AWR report with an unknown error.
 
Possible Causes: This error may appear due to issue(s) in parsing the AWR report provided by the user.
 
Remediation actions: Try to create a new assessment with another AWR report. Also, you may try running the assessment while connected to Oracle instance. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO). 
 

 
### Error Code: ODC-1002

Error Message: Connection timed out to the Oracle server. Navigate to the connections panel and refresh the connection to the Oracle server.
 
Possible causes: This error may appear due to any of the given reasons-
1)	Oracle server connection terminated forcefully or timed-out.
2)	Network connectivity to the Oracle server is unstable or lost.

Remediation actions: Try to reconnect to the Oracle server or refresh the connection. Also, ping the Oracle server to check the network connectivity between your machine and Oracle server is stable. Check if the timeout parameters are set properly in sqlnet.ora file. For more details, refer Oracle documentation for ORA-03135 error.
 

 
### Error Code: SQLA-3001
 
Error Message: SKU recommendation for Azure SQL target platform failed with an unknown error.
 
Possible causes: The SKU recommendation failed to process the performance data.
 
Remediation actions: Try to create a new assessment. To investigate further, go through the [logs](https://aka.ms/logsDMAO) to understand the reason of error. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO). 



### Error Code: AWR-0001
 
Error Message: The specified AWR report version does not match with the connected Oracle server. Run a new assessment with an AWR report of the same server.
 
Possible causes: The version of the Oracle instance in the AWR report is different from the version of connected Oracle server.
 
Remediation actions: You must upload the correct AWR report for the connected oracle server so that the AWR report and Oracle instance’s version matches and then try to run a new assessment.
If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO). 


 
### Error Code: AWR-0002
 
Error Message: Uploaded report is not a valid AWR report. Run a new assessment with a valid AWR report of the connected server.
 
Possible causes: The name of the Oracle instance in the AWR report is different from the connected Oracle server.
 
Remediation actions: You must upload the correct AWR report for the connected Oracle server so that the AWR report and Oracle instance’s name matches and then try to run a new assessment.
If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO).
 

 
### Error Code: ORADA-1002
 
Error Message: Assessment failed as it received an invalid value={invalid value} for metric name={metric name} from the Oracle server.
 
Possible causes: This error may appear due to any of the given reasons-
1)	The state of performance data available in the Oracle server is invalid.
2)	The collected performance data file is corrupted or modified externally.
 
Remediation actions: Try to create a new assessment. To investigate further, go through the [logs](https://aka.ms/logsDMAO) to understand the reason of error. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO).



### Error Code: DBNS-0000
 
Error Message: Assessment failed as {0} database running on {1} platform with version {2} is unsupported. For supported versions, refer aka.ms/prereq
 
Possible Causes: Database Migration Assessment for Oracle extension supports 10g or higher Oracle versions. 

Remediation actions: Try to create a new assessment on a supported version of Oracle server. Current version is unsupported. Refer to [prerequisites](https://aka.ms/prereq).
 


### Error Code: AWR-0000
 
Error Message: Oracle database name in the specified AWR report doesn't matches with the connected Oracle server. Run a new assessment with an AWR report of the same database.
 
Possible causes: The Oracle database name in the AWR report is different from the connected Oracle database server.
 
Remediation actions: You must upload the correct AWR report for the connected Oracle server so that the AWR report and Oracle database server name matches and then try to create a new assessment. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO). 

 
 
### Error Code: GE-1002
 
Error Message: Configured assessment folder doesn't exist.
 
Possible Causes: Assessment folder path provided doesn't exist locally.

Remediation actions: Verify the assessment folder path exists on local computer. In case you've changed the assessment data folder path, check the current assessment path mentioned in extension settings is correct.

 

### Error Code: ODC-1005

Error Message: Connected oracle server is not available due to an underlying Oracle exception - ORA1034.

Possible Causes: Database might be down or not ready to accept connections.

Remediation actions: Check if the database is online, in a healthy state and ready to accept new connections. For more information, see Oracle documentation for ORA-1034 error. 
 

 
### Error Code: ODC-1003

Error Message: Login failed due to invalid credentials. Verify the login credentials for database and try again.

Possible Causes: Login failure might be due to invalid credentials.

Remediation actions: You must ensure that the Oracle Database credentials provided are correct. Verify the login credentials for database and try again.
 
 
 
### Error Code: ODC-1004 
 
Error Message: Failed to connect to the Oracle server due to an underlying driver error code {error code}.
   
Possible Causes: This error may appear due to any of the given reasons-
1.	Oracle server connection failed due to TNS connectivity failure.
2.	Network connectivity to the Oracle server is unstable or lost.

Remediation actions: Try to reconnect to the Oracle server or refresh the connection. Also, ping the Oracle server to check the network connectivity between your machine and Oracle server is stable.


  
### Error Code: ODC-0000
 
Error Message: The data collection failed with an unknown error. Try creating a new assessment.
 
Possible Causes: This error is generic and may appear due to multiple reasons.

Remediation actions: Try to create a new assessment. To investigate further, go through the [logs](https://aka.ms/logsDMAO) to understand the reason of error.  If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO). 

 
 
### Error Code: ODC-0004
 
Error Message: Ora2Pg code complexity data collection failed with an unknown error. Try creating a new assessment.

Possible Causes: This error may appear when Ora2pg data collection failed due to an unhandled exception.

Remediation actions: Try to create a new assessment. To investigate further, go through the [logs](https://aka.ms/logsDMAO) to understand the reason of error.  If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO).
 

 
### Error Code: Ora2pg-2000
 
Error Message: Failed to parse the Ora2Pg assessment report with an unknown error. Try creating a new assessment.

Possible Causes: This error may appear during the parsing of Ora2pg report due to an unhandled exception.

Remediation actions: Try to create a new assessment. To investigate further, go through the [logs](https://aka.ms/logsDMAO) to understand the reason of error.  If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO).



### Error Code: MDS-0004

Error Message: Failed to save the assessment output with a serializer error.

Possible Causes: This error may appear due to any of the given reasons-
1.	The metadata file is either corrupted or modified externally.
2.	Couldn't parse the json for the metadata file correctly.

Remediation actions: You may have to delete the current assessment and then try creating a new assessment. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO). 
 

 
### Error Code: MDS-0005

Error Message: Failed to delete the assessment report with an unknown error.

Possible Causes: This error may appear due to any of the given reasons-
1.	The user may not have the appropriate permission to delete files from the assessment data folder.
2.	The assessment data files are already in use by another program.
 
Remediation actions: You need to ensure that the logged in user has delete permissions to the assessment data folder. In case you've changed the assessment data folder path, check the current assessment path mentioned in extension settings is correct. Try to create a new assessment after closing any open assessment file(s).

 

### Error Code: FSRP-0000

Error Message: A terminal error occurred - failed to access lookup data. A lookup file is either deleted or corrupted. Reinstall the extension and try again.

Possible Causes: This error may appear due to any of the given reasons-
1.	The lookup metadata file is either deleted or corrupted. 
2.	The lookup metadata file is already in use by another program.
 
Remediation actions: Try to reinstall the extension, close any open assessment file(s), and then create a new assessment. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO).
 
 
 
### Error Code: LDR-0000

Error Message: A terminal error occurred - failed to access lookup data. A lookup file is either deleted or corrupted. Reinstall the extension and try again.

Possible Causes: This error may appear due to any of the given reasons-
1.	The lookup metadata file is either deleted or corrupted. 
2.	The lookup metadata file is already in use by another program.
 
Remediation actions: Try to reinstall the extension, close any open assessment file(s), and then create a new assessment. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO).
 


### Error Code: LDR-0001

Error Message: A terminal error occurred - failed to parse lookup data. A lookup file is either deleted or corrupted. Reinstall the extension and try again.

Possible Causes: This error may appear due to any of the given reasons-
1.	The lookup metadata file is either deleted or corrupted. 
2.	The lookup metadata file is already in use by another program.
 
Remediation actions: Try to reinstall the extension, close any open assessment file(s), and then create a new assessment. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO).
 

 
### Error Code: ORADA-0000

Error Message: Failed to aggregate the collected performance data with to an unknown error.

Possible Causes: This error is an uncategorized error.

Remediation actions: To investigate further, go through the [logs](https://aka.ms/logsDMAO) to understand the reason of error. [contact Microsoft for help](https://aka.ms/contactDMAO) 
 
 
### Error Code: ODC-0002

Error Message: Failed to collect Oracle feature data with to an unknown error.

Possible Causes: This error is an uncategorized error.

Remediation actions: To investigate further, go through the [logs](https://aka.ms/logsDMAO) to understand the reason of error. [contact Microsoft for help](https://aka.ms/contactDMAO) 
 
 
 
### Error Code: FSMB-0000

Error Message: Failed to generate the assessment output with an unknown error.
  
Possible Causes: This error may appear due to any of the given reasons-
1.	The user may not have the permission to write to the assessment data folder.
2.	The assessment data folder is already in use by another program.
 
Remediation actions: You need to ensure that the logged in user has write permissions on the assessment data folder. In case you've changed the assessment data folder path, check the current assessment path mentioned in extension settings is correct. Try to create a new assessment after closing any open assessment file(s).



### Error Code: FSMB-0004

Error Message: Failed to save the assessment output with a serializer error.
 
Possible Causes: The generated assessment data Couldn't be converted to JSON format. 
 
Remediation actions: You may try to run a new assessment. To investigate further, go through the [logs](https://aka.ms/logsDMAO) to understand the reason of error. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO)



### Error Code: FSMB-0005

Error Message: Failed to delete the assessment report with a file system error.
 
Possible Causes: This error may appear due to any of the given reasons-
1.	The user may not have the appropriate permission to delete files from the assessment data folder.
2.	The assessment data files are already in use by another program.
 
Remediation actions: You need to ensure that the logged in user has delete permissions on the assessment data folder. In case you've changed the assessment data folder path, check the current assessment path mentioned in extension settings is correct. Try to create a new assessment after closing any open assessment file(s).



### Error Code: MDS-0001

Error Message: Failed to read the assessment data with a file system error.
 
Possible Causes: This error may appear due to any of the given reasons-
1.	The user may not have the appropriate permission to access the assessment data folder.
2.	The assessment data folder doesn't exist or is in use by another program.
 
Remediation actions: You need to ensure that the logged in user has read and write permissions on the assessment data folder. In case you've changed the assessment data folder path, check the current assessment path mentioned in extension settings is correct. 



### Error Code: MDS-0002

Error Message: Failed to read the assessment data with a serializer error.
 
Possible Causes: This error may appear due to any of the given reasons-
1.	The user may not have the appropriate permission to access the assessment data folder.
2.	The assessment data folder doesn't exist or is in use by another program.
3.	The assessment data was generated by an older version of ADS extension that isn't supported anymore.
 
Remediation actions: You need to ensure that the logged in user has read and write permissions on the assessment data folder. In case you've changed the assessment data folder path, check the current assessment path mentioned in extension settings is correct. Try to delete the current assessment and then run a new assessment.

 

### Error Code: ORADA - 1001

Error Message: Failed to process collected static data with serializer error. Try creating a new assessment.
 
Possible Causes: This error may appear due to any of the given reasons-
1.	Unable to populate performance counter from static data.
2.	Instance or database configs are unavailable or not in the required format.
 
Remediation actions: You may try to run a new assessment. To investigate further, go through the [logs](https://aka.ms/logsDMAO) to understand the reason of error. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO)



### Error Code: ORADA-1000

Error Message: Failed to process collected performance data with serializer error. Try creating a new assessment.
 
Possible Causes: This error may appear due to any of the given reasons-
1.	Unable to populate performance counter from static data.
2.	Instance or database configs are unavailable or not in the required format.
 
Remediation actions: Try to create a new assessment. To investigate further, go through the [logs](https://aka.ms/logsDMAO) to understand the reason of error. If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO)
 

 
### Error Code: AWR-0003

Error Message: Uploaded AWR report is for a pluggable database, but a CDB root container AWR report was expected for performance data collection. Try creating a new assessment with the AWR report for CDB root container.
 
Possible causes: The uploaded AWR report is for a Pluggable database. You need to provide a CDB root container AWR report for performance data collection.
 
Remediation actions: Try creating a new assessment with the AWR report for CDB root container.
If the issue persists, [contact Microsoft for help](https://aka.ms/contactDMAO)

