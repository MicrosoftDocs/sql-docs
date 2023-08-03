---
title: Troubleshooting the Azure PostgreSQL migration extension errors in Azure Data Studio
description: This article explains how to determine, diagnose, and fix issues/errors that you might encounter when you use Azure PostgreSQL migration extension.
author: apduvuri
ms.author: adityaduvuri
ms.reviewer: maghan, randolphwest
ms.date: 02/20/2023
ms.service: azure-data-studio
ms.topic: troubleshooting
---

# Troubleshoot Azure PostgreSQL migration extension errors

This article explains how to determine, diagnose, and fix issues/errors you might encounter when using the Azure PostgreSQL migration extension.

## Troubleshoot

To troubleshoot any Azure PostgreSQL migration extension issue, you should find out the details about the error and warnings from the logs generated. Refer to this section to access the logs.

### Logs

The extension stores errors, warnings, and other diagnostic logs in the default log directory:

- Windows - `C:\Users\<username>\.postgresmigration\logs\`
- Linux - `~/.postgresmigration/logs`
- macOS - `/Users/<username>/.postgresmigration/logs`

> [!NOTE]  
> By default, the extension stores the last seven log files.

## Error Codes Details

The below section gives you more details about error codes, messages, possible causes, and their respective remediation actions.

### Error Code: GE-0000

Error Message: The data collection failed with an unknown error. Try creating a new assessment.

Possible Causes: This error is generic and may appear for multiple reasons.

Remediation actions: Try to create a new assessment. To investigate further, go through the [logs](#logs) to understand the reason for the error. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: FSMB-0000

Error Message: Failed to generate the assessment output with an unknown error.

Possible Causes: This error may appear due to any of the given reasons-

1. The user may not have permission to write to the assessment data folder.

1. The assessment data folder is already in use by another program.

Remediation actions: You must ensure that the logged-in user has written permissions on the assessment data folder. If you've changed the assessment data folder path, check if the current assessment path mentioned in the extension settings is correct. Try to create a new assessment after closing any open assessment file(s).

### Error Code: FSMB-0001

Error Message: Failed to access assessment data.

Possible causes: This error may appear due to any of the given reasons-

1. The user may need the appropriate permission to access the assessment data folder.

1. The assessment data folder doesn't exist or is in use by another program.

Remediation actions: You must ensure that the logged-in user has read and write permissions on the assessment data folder. If you've changed the assessment data folder path, check if the current assessment path mentioned in the extension settings is correct.

### Error Code: FSMB-0002

Error Message: Failed to parse the assessment data. A metadata file is either corrupted or modified externally. Try creating a new assessment.

Possible Causes: This error may appear due to any of the given reasons-

1. The metadata file is either corrupted or modified externally.

1. Need help with parsing the JSON for the metadata file correctly.

Remediation actions: You may have to delete the current assessment and then try creating a new assessment. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: FSMB-0003

Error Message: Failed to save the assessment output with a file system error.

Possible Causes: This error may appear due to any of the given reasons-

1. The user may need the appropriate permission to use the assessment data folder.

1. The assessment data folder is already in use by another program.

Remediation actions: You must ensure that the logged-in user has read and written permissions to the assessment data folder. In case you've changed the assessment data folder path, check the current assessment path mentioned in the extension settings is correct and exists. Try to create a new assessment after closing any open assessment file(s).

### Error Code: FSMB-0004

Error Message: Failed to save the assessment output with a serializer error.

Possible Causes: The generated assessment data Couldn't be converted to JSON format.

Remediation actions: You may try to run a new assessment. To investigate further, go through the [logs](#logs) to understand the reason for the error. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: MDS-0001

Error Message: Failed to read the assessment data with a file system error.

Possible Causes: This error may appear due to any of the given reasons-

1. The user may need the appropriate permission to access the assessment data folder.

1. The assessment data folder doesn't exist or is in use by another program.

Remediation actions: You must ensure that the logged-in user has read and write permissions on the assessment data folder. If you've changed the assessment data folder path, check if the current assessment path mentioned in the extension settings is correct.

### Error Code: MDS-0002

Error Message: Failed to read the assessment data with a serializer error.

Possible Causes: This error may appear due to any of the given reasons-

1. The user may need the appropriate permission to access the assessment data folder.

1. The assessment data folder doesn't exist or is in use by another program.

1. The assessment data was generated by an older version of the extension that isn't supported anymore.

Remediation actions: You must ensure that the logged-in user has read and write permissions on the assessment data folder. If you've changed the assessment data folder path, check if the current assessment path mentioned in the extension settings is correct. Try to delete the current assessment and then run a new assessment.

### Error Code: MDS-0003

Error Message: Failed to save the assessment output with an unknown error.

Possible causes: This error may appear due to any of the given reasons-

1. The user may need the appropriate permission to access the chosen folder for saving the assessment.

1. The folder is already in use by another program.

Remediation actions: You must ensure that the logged-in user has read and write permissions on the assessment data folder. If you've changed the assessment data folder path, check if the current assessment path mentioned in the extension settings is correct. And then, try to create a new assessment after closing any open assessment file(s).

### Error Code: MDS-0004

Error Message: Failed to save the assessment output with a serializer error.

Possible Causes: This error may appear due to any of the given reasons-

1. The metadata file is either corrupted or modified externally.

1. Need help with parsing the JSON for the metadata file correctly.

Remediation actions: You may have to delete the current assessment and then try creating a new assessment. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: FSRP-0000

Error Message: A terminal error occurred - failed to access lookup data. A lookup file is either deleted or corrupted. Reinstall the extension and try again.

Possible Causes: This error may appear due to any of the given reasons-

1. The lookup metadata file is either deleted or corrupted.

1. The lookup metadata file is already in use by another program.

Remediation actions: Try reinstalling the extension, close any open assessment file(s), and create a new assessment. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: FSRP-0001

Error Message: A terminal error occurred - failed to access lookup data. A lookup file is either deleted or corrupted. Reinstall the extension and try again.

Possible Causes: This error may appear due to any of the given reasons-

1. The lookup metadata file is either deleted or corrupted.

1. The lookup metadata file is already in use by another program.

Remediation actions: Try reinstalling the extension, close any open assessment file(s), and create a new assessment. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: GE-0001

Error Message: Failed to run one or more assessment components with an unknown error.

Possible causes: An unknown internal error has occurred.

Remediation actions: Try to create a new assessment. To investigate further, go through the [log](./database-migration-assessment-for-oracle-extension.md) to understand the reason for the error. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: GE-1002

Error Message: Configured assessment folder doesn't exist.

Possible Causes: The assessment folder path provided doesn't exist locally.

Remediation actions: Verify the assessment folder path exists on the local computer. If you've changed the assessment data folder path, check if the current assessment path mentioned in the extension settings is correct.

### Error Code: PGSQLC - 0000

Error Message: The data collection failed with an unknown error. Try creating a new assessment.

Possible Causes: This error is generic and may appear for multiple reasons.

Remediation actions: Try to create a new assessment. To investigate further, go through the [logs](#logs) to understand the reason for the error. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: PGSQLC-0001

Error Message: Failed to collect PostgreSQL Server Compatibility data with an unknown error.

Possible Causes: This error occurs during the collection of Server Compatibility data.

Remediation actions: To investigate further, go through the [logs](#logs) to understand the reason for the error. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: PGSQLC-0002

Error Message: Failed to collect PostgreSQL Database Compatibility data with an unknown error.

Possible Causes: This error occurs during the collection of Database Compatibility data.

Remediation actions: To investigate further, go through the [logs](#logs) to understand the reason for the error. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: PGSQLC-0003

Error Message: Failed to process the performance data.

Possible causes: This Error occurred during the collection of PostgreSQL performance data.

Remediation actions: Try to create a new assessment. To investigate further, go through the [log](./database-migration-assessment-for-oracle-extension.md) to understand the reason for the error. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: PGSQLA-1000

Error Message: The server Compatibility module for the Azure PostgreSQL target platform failed with an unknown error.

Possible causes: The Server Compatibility module failed to process the server compatibility data.

Remediation actions: Try to create a new assessment. To investigate further, go through the [logs](#logs) to understand the reason for the error. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: PGSQLA-2000

Error Message: Database Compatibility Data for the Azure PostgreSQL target platform failed with an unknown error.

Possible causes: The Database Compatibility module failed to process the database compatibility data.

Remediation actions: Try to create a new assessment. To investigate further, go through the [logs](#logs) to understand the reason for the error. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: PGSQLA-3001

Error Message: SKU recommendation for Azure PostgreSQL target platform failed with an unknown error.

Possible causes: The SKU recommendation failed to process the performance data.

Remediation actions: Try to create a new assessment. To investigate further, go through the [logs](#logs) to understand the reason for the error. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: PGSQLDA-0001

Error Message: Failed to aggregate the collected performance data details with an unknown error.

Possible Causes: Failed to process the performance data.

Remediation actions: To investigate further, go through the [logs](#logs) to understand the reason for the error. [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: PGDC-10000

Error Message: Failed to collect required data with an underlying PG error. Check Postgres documentation for this error code.

Possible Causes: Assessment failed due to an underlying PostgreSQL Error

Remediation actions: You may need to refer to PG documentation to resolve this issue. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: PGDC-10001

Error Message: Connection lost. Check your network connectivity. Check Postgres documentation for this error code.

Possible Causes: Assessment failed due to network connectivity issue.

Remediation actions: You may need to refer to PG documentation to resolve this issue. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: PGDC-10002

Error Message: Connection timed out to the PostgreSQL Server. Navigate to the connections panel and refresh the connection to the PostgreSQL Server.

Possible causes: This error may appear due to any of the given reasons-

1. PostgreSQL Server connection terminated forcefully or timed out.

1. Network connectivity to the PostgreSQL Server is unstable or lost.

Remediation actions: Try to reconnect to the PostgreSQL Server or refresh the connection. Also, ping the PostgreSQL Server to check the network connectivity between your machine and the PostgreSQL Server is stable. Check if the timeout parameters are correctly set in postgresql.conf file.

### Error Code: PGDC-10003

Error Message: Login failed due to invalid credentials. Verify the login credentials for the database and try again.

Possible Causes: Login failure might be due to invalid credentials.

Remediation actions: You must ensure that the PostgreSQL Server credentials provided are correct. Verify the login credentials for the database and try again.

### Error Code: PGDC-10004

Error Message: Connected PostgreSQL Server is unavailable due to an underlying PG exception.

Possible Causes: The database might be down or unwilling to accept connections.

Remediation actions: Check if the database is online, healthy, and ready to accept new connections. For more information, see Postgres documentation.

### Error Code: PGDC-10005

Error Message: The user doesn't have the privilege to access the table/view for Assessment.

Possible Causes: This error may appear due to any of the given reasons -

1. Lack of Connect privilege.

1. Lack of Select privilege.

Remediation actions: Provide the required permission to the Assessment user. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

### Error Code: PGDC-10006

Error Message: No such host is known. Connect to a valid host.

Possible Causes: The host isn't valid. Either the IP Address/hostname is incorrect, or the port number is wrong.

Remediation actions: Check if the hostname and port number are correct. If the issue persists, [contact Microsoft for help](azure-postgresql-migration-extension.md#get-help-from-microsoft-support).

## Next steps

- [Azure PostgreSQL Migration extension](azure-postgresql-migration-extension.md)
- [PostgreSQL extension](postgres-extension.md)
- [Azure Data Studio extensions](add-extensions.md)
- [Azure Data Studio release notes](../release-notes-azure-data-studio.md)
- [Database migrations using Azure Data Studio](/azure/dms/migration-using-azure-data-studio)
- [Download Azure Data Studio](../download-azure-data-studio.md)