---
title: VDI Backup Specification - SQL Server on Linux | Microsoft Docs
description: SQL Server Backup Virtual Device Interface Specification.
author: MikeRayMSFT 
ms.author: mikeray 
manager: craigg
ms.date: 03/17/2017
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: 0250ba2b-8cdd-450e-9109-bf74f70e1247
---
# SQL Server on Linux VDI client SDK Specification

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This document covers the interfaces provided by the SQL Server on Linux virtual device interface (VDI) client SDK. Independent software vendors (ISVs) can use the Virtual Backup Device Application Programming Interface (API) to integrate SQL Server into their products. In general, VDI on Linux behaves similarly to VDI on Windows with the following changes:

- Windows Shared Memory becomes POSIX shared memory.
- Windows Semaphores become POSIX semaphores.
- Windows types like HRESULT and DWORD are changed to integer equivalents.
- The COM interfaces are removed and replaced with a pair of C++ Classes.
- SQL Server on Linux does not support named instances so references to instance name have been removed. 
- The shared library is implemented in libsqlvdi.so installed at /opt/mssql/lib/libsqlvdi.so

This document is an addendum to **vbackup.chm** that details the Windows VDI Specification. Download the [Windows VDI Specification](https://www.microsoft.com/download/details.aspx?id=17282).

Also review the sample VDI backup solution on the [SQL Server Samples GitHub repository](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/sqlvdi-linux).

## User Permissions Setup

On Linux, POSIX primitives are owned by the user creating them and their default group. For objects created by SQL Server, these will by default be owned by the mssql user and the mssql group. To allow sharing between SQL Server and the VDI client, one of the following two methods are recommended:

1. Run the VDI Client as the mssql user
   
   Execute the following command to switch to mssql user:
   
   ```bash
   sudo su mssql
   ```

2. Add the mssql user to the vdiuser's group, and the vdiuser to the mssql group.
   
   Execute the following commands:

   ```bash
   sudo useradd vdiuser
   sudo usermod -a -G mssql vdiuser
   sudo usermod -a -G vdiuser mssql
   ```

   Restart the server to pick up new groups for SQL Server and vdiuser

## Client Functions

This chapter contains descriptions of each of the client functions. The descriptions include the following information:

- Function purpose
- Function syntax
- Parameter list
- Return values
- Remarks

## ClientVirtualDeviceSet::Create

**Purpose** This function creates the virtual device set.

**Syntax**
   ```
   int ClientVirtualDeviceSet::Create (
   char *	name,		// name for the set
   VDConfig   *	cfg		// configuration for the set
   );
   ```

| Parameters | Argument | Explanation
| ----- | ----- | ------ |
| | **name** | This identifies the virtual device set. The rules for names used by CreateFileMapping() must be followed. Any character except backslash (\) may be used. This is a character string. Prefixing the string with the user's product or company name and database name is recommended. |
| |**cfg** | This is the configuration for the virtual device set. For more information, see "Configuration" later in this document.

| Return Values | Argument | Explanation
| ----- | ----- | ------ |
| |**NOERROR** | The function succeeded. |
| |**VD_E_NOTSUPPORTED** |One or more of the fields in the configuration was invalid or otherwise unsupported. |
| |**VD_E_PROTOCOL** | The virtual device set already exists.

**Remarks** The Create method should be called only once per BACKUP or RESTORE operation. After invoking the Close method, the client can reuse the interface to create another virtual device set.

## ClientVirtualDeviceSet::GetConfiguration

**Purpose**	This function is used to wait for the server to configure the virtual device set.
**Syntax**
   ```
   int ClientVirtualDeviceSet::GetConfiguration (
   time_t	 	timeout,	// in milliseconds
   VDConfig *		cfg	// selected configuration
   );
   ```

| Parameters | Argument | Explanation
| ----- | ----- | ------ |
| | **timeout** | This is the time-out in milliseconds. Use INFINITE or any negative integer to prevent time-out.
| | **cfg** | Upon successful execution, this contains the configuration selected by the server. For more information, see "Configuration" later in this document.

| Return Values | Argument | Explanation
| ----- | ----- | ------ |
| |**NOERROR** | The configuration was returned.
| |**VD_E_ABORT** |SignalAbort was invoked.
| |**VD_E_TIMEOUT** |The function timed out.

**Remarks** This function blocks in an Alertable state. After successful invocation, the devices in the virtual device set may be opened.


## ClientVirtualDeviceSet::OpenDevice
**Purpose** This function opens one of the devices in the virtual device set.
**Syntax**
   ```
   int ClientVirtualDeviceSet::OpenDevice (
   char *			name,		// name for the set
   ClientVirtualDevice **		ppVirtualDevice	// returns interface to device
   );
   ```

| Parameters | Argument | Explanation
| ----- | ----- | ------ |
| | **name** |This identifies the virtual device set.
| | **ppVirtualDevice** |When the function succeeds, a pointer to the virtual device is returned. This device is used for GetCommand and CompleteCommand.

| Return Values | Argument | Explanation
| ----- | ----- | ------ |
| |**NOERROR** |The function succeeded.
| |**VD_E_ABORT** |	Abort was requested.
| |**VD_E_OPEN** |	All devices are open.
| |**VD_E_PROTOCOL** |	The set is not in the initializing state or this particular device is already open.
| |**VD_E_INVALID** |The device name is invalid. It is not one of the names known to comprise the set.

**Remarks** VD_E_OPEN may be returned without problem. The client may call OpenDevice by means of a loop until this code is returned.
If more than one device is configured, for example *n* devices, the virtual device set will return *n* unique device interfaces.

The `GetConfiguration` function can be used to wait until the devices can be opened.
If this function does not succeed, then a null value is returned through the ppVirtualDevice.
 
## ClientVirtualDevice::GetCommand

**Purpose** This function is used to obtain the next command queued to a device. When requested, this function waits for the next command.

**Syntax**
   ```
   int ClientVirtualDevice::GetCommand (
   time_t	 	timeout,	// time-out in milliseconds
   VDC_Command**	ppCmd	// returns the next command
   );
   ```

| Parameters | Argument | Explanation
| ----- | ----- | ------ |
| |**timeout** |This is the time to wait, in milliseconds. Use INFINTE to wait indefinitely. Use 0 to poll for a command. VD_E_TIMEOUT is returned if no command is currently available . If the time-out occurs, the client decides the next action.
| |**Timeout** |This is the time to wait, in milliseconds. Use INFINTE or a negative value to wait indefinitely. Use 0 to poll for a command. VD_E_TIMEOUT is returned if no command is available before the timeout expires. If the timeout occurs, the client decides the next action.
| |**ppCmd** |When a command is successfully returned, the parameter returns the address of a command to execute. The memory returned is read-only. When the command is completed, this pointer is passed to the CompleteCommand routine. For details about each command, see "Commands" later in this document.
		
| Return Values | Argument | Explanation
| ----- | ----- | ------ |
| |**NOERROR** |A command was fetched.
| |**VD_E_CLOSE** |The device has been closed by the server.
| |**VD_E_TIMEOUT** |No command was available and the time-out expired.
| |**VD_E_ABORT** |Either the client or the server has used the SignalAbort to force a shutdown.

**Remarks**	When VD_E_CLOSE is returned, SQL Server has closed the device. This is part of the normal shutdown. After all devices have been closed, the client invokes ClientVirtualDeviceSet::Close to close the virtual device set.
When this routine must block to wait for a command, the thread is left in an Alertable condition.

## ClientVirtualDevice::CompleteCommand

**Purpose**	This function is used to notify SQL Server that a command has finished. Completion information appropriate for the command should be returned. For more information, see "Commands" later in this document.

**Syntax** 

   ```
   int ClientVirtualDevice::CompleteCommand (
   VDC_Command pCmd,		// the command
   int 	completionCode,		// completion code
   unsigned long 	bytesTransferred,	// bytes transferred
   int64_t 	position		// current position
   );
   ```

| Parameters | Argument | Explanation
| ----- | ----- | ------ |
| |**pCmd** |This is the address of a command previously returned from ClientVirtualDevice::GetCommand.
| |**completionCode** |This is a status code that indicates the completion status. This parameter must be returned for all commands. The code returned should be appropriate to the command being performed. ERROR_SUCCESS is used in all cases to denote a successfully executed command. For the complete list of possible codes, see the file, vdierror.h. A list of typical status codes for each command appears in "Commands" later in this document.
| |**bytesTransferred** |This is the number of successfully transferred bytes. This is returned only for data transfer commands Read and Write.
| |**position** |This is a response to the GetPosition   command only.
		
| Return Values | Argument | Explanation
| ----- | ----- | ------ |
| |**NOERROR** |The completion was correctly noted.
| |**VD_E_INVALID** |pCmd was not an active command.
| |**VD_E_ABORT** |Abort was signaled.
| |**VD_E_PROTOCOL** |The device is not open.

**Remarks** None

## ClientVirtualDeviceSet::SignalAbort

**Purpose**	This function is used to signal that an abnormal termination should occur.

**Syntax** 

   ```
   int ClientVirtualDeviceSet::SignalAbort ();
   ```

| Parameters | Argument | Explanation
| ----- | ----- | ------ |
| |None	| Not applicable
		
| Return Values | Argument | Explanation
| ----- | ----- | ------ |
| |**NOERROR**|The Abort notification was successfully posted.

**Remarks** At any time, the client may choose to abort the BACKUP or RESTORE operation. This routine signals that all operations should cease. The state of the overall virtual device set enters an Abnormally Terminated state. No further commands are returned on any devices. All uncompleted commands are automatically completed, returning ERROR_OPERATION_ABORTED as a completion code. The client should call ClientVirtualDeviceSet::Close after it has safely terminated any outstanding use of buffers provided to the client. For more information, see "Abnormal Termination" earlier in this document.

## ClientVirtualDeviceSet::Close

**Purpose**	This function closes the virtual device set created by ClientVirtualDeviceSet::Create. It results in the release of all resources associated with the virtual device set.

**Syntax** 

   ```
   int ClientVirtualDeviceSet::Close ();
   ```

| Parameters | Argument | Explanation
| ----- | ----- | ------ |
| |None |Not applicable
		
| Return Values | Argument | Explanation
| ----- | ----- | ------ |
| |**NOERROR** |This is returned when the virtual device set was successfully closed.
| |**VD_E_PROTOCOL** |No action was taken because the virtual device set was not open.
| |**VD_E_OPEN** |Devices were still open.

**Remarks**	The invocation of Close is a client declaration that all resources used by the virtual device set should be released. The client must ensure that all activity involving data buffers and virtual devices is terminated before invoking Close. All virtual device interfaces returned by OpenDevice are invalidated by Close.
The client is permitted to issue a Create call on the virtual device set interface after the Close call is returned. Such a call would create a new virtual device set for a subsequent BACKUP or RESTORE operation.
If Close is called when one or more virtual devices are still open, VD_E_OPEN is returned. In this case, SignalAbort is internally triggered, to ensure a proper shutdown if possible. VDI resources are released. The client should wait for a VD_E_CLOSE indication on each device before invoking ClientVirtualDeviceSet::Close. If the client knows that the virtual device set is already in an Abnormally Terminated state, then it should not expect a VD_E_CLOSE indication from GetCommand, and may invoke ClientVirtualDeviceSet::Close as soon as activity on the shared buffers is terminated.
For more information, see "Abnormal Termination" earlier in this document.

## ClientVirtualDeviceSet::OpenInSecondary

**Purpose**	This function opens the virtual device set in a secondary client. The primary client must have already used Create and GetConfiguration to set up the virtual device set.

**Syntax** 
   
   ```
   int ClientVirtualDeviceSet::OpenInSecondary (
   char *	setName			// name of the set
   );
   ```

| Parameters | Argument | Explanation
| ----- | ----- | ------ |
| |**setName** |This identifies the set. This name is case-sensitive and must match the name used by the primary client when it invoked ClientVirtualDeviceSet::Create.

| Return Values | Argument | Explanation
| ----- | ----- | ------ |
| |**NOERROR** |The function succeeded.
| |**VD_E_PROTOCOL** |The virtual device set has not been created, has already been opened on this client, or the virtual device set is not ready to accept open requests from secondary clients.
| |**VD_E_ABORT** |The operation is being aborted.

**Remarks** When using a multiple process model, the primary client is responsible for detecting normal and abnormal termination of secondary clients.

## ClientVirtualDeviceSet::GetBufferHandle

**Purpose**	Some applications may require more than one process to operate on the buffers returned by ClientVirtualDevice::GetCommand. In such cases, the process that receives the command can use GetBufferHandle to obtain a process independent handle that identifies the buffer. This handle can then be communicated to any other process that also has the same Virtual Device Set open. That process would then use ClientVirtualDeviceSet::MapBufferHandle to obtain the address of the buffer. The address will likely be a different address than in its partner because each process may be mapping buffers at different addresses.

**Syntax** 

   ```
   int ClientVirtualDeviceSet::GetBufferHandle (
   uint8_t*		pBuffer,		// in: buffer address
   unsigned int*		pBufferHandle	// out: buffer handle
   );
   ```

| Parameters | Argument | Explanation
| ----- | ----- | ------ |
| |**pBuffer** |This is the address of a buffer obtained from a Read or Write command.
| |**BufferHandle** |A unique identifier for the buffer is returned.

| Return Values | Argument | Explanation
| ----- | ----- | ------ |
| |**NOERROR** |The function succeeded.
| |**VD_E_PROTOCOL** |The virtual device set is not currently open.
| |**VD_E_INVALID** |The pBuffer is not a valid address.
Remarks	The process that invokes the GetBufferHandle function is responsible for invoking ClientVirtualDevice::CompleteCommand when the data transfer is complete.

## ClientVirtualDeviceSet::MapBufferHandle

**Purpose**	This function is used to obtain a valid buffer address from a buffer handle obtained from some other process. 

**Syntax** 

   ```
   int ClientVirtualDeviceSet::MapBufferHandle (
   i		nt 	dwBuffer,	// in: buffer handle
   uint8_t**	ppBuffer		// out: buffer address
   );
   ```

| Parameters | Argument | Explanation
| ----- | ----- | ------ |
| |**dwBuffer** |This is the handle returned by ClientVirtualDeviceSet::GetBufferHandle.
| |**ppBuffer** |This is the address of the buffer that is valid in the current process.

| Return Values | Argument | Explanation
| ----- | ----- | ------ |
| |**NOERROR** |The function succeeded.
| |**VD_E_PROTOCOL** |The virtual device set is not currently open.
| |**VD_E_INVALID** |The ppBuffer is an invalid handle.

**Remarks**	Care must be taken to communicate the handles correctly. Handles are local to a single virtual device set. The partner processes sharing a handle must ensure that buffer handles are used only within the scope of the virtual device set from which the buffer was originally obtained.


