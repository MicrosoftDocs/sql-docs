---
title: SQL Server on Linux VDI Client SDK Specification
description: Learn about the interfaces provided by the SQL Server on Linux virtual device interface (VDI) client SDK.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 08/11/2026
ms.service: sql
ms.subservice: linux
ms.topic: concept-article
ms.custom:
  - linux-related-content
---
# SQL Server on Linux VDI client SDK specification

[!INCLUDE [SQL Server - Linux](../../../includes/applies-to-version/sql-linux.md)]

This article covers the interfaces provided by the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on Linux virtual device interface (VDI) client SDK.

> [!NOTE]  
> For [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] on Linux, you can [create a Transact-SQL snapshot backup](../../../relational-databases/databases/create-a-database-snapshot-transact-sql.md) instead.

Independent software vendors (ISVs) can use the Virtual Backup Device application programming interface (API) to integrate [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] into their products. VDI on Linux behaves similarly to VDI on Windows with the following changes:

- Windows shared memory becomes POSIX shared memory.
- Windows semaphores become POSIX semaphores.
- Windows types like `HRESULT` and `DWORD` become integer equivalents.
- The COM interfaces are replaced by a pair of C++ classes.
- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on Linux doesn't support named instances, so references to instance name are removed.
- The shared library is implemented in `libsqlvdi.so`, installed at `/opt/mssql/lib/libsqlvdi.so`.

This article is an addendum to [Virtual device interface (VDI) reference](../../../relational-databases/backup-restore/vdi-reference/reference-virtual-device-interface.md) that details the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] VDI Specifications on Windows.

Also review the sample VDI backup solution on the [SQL Server Samples GitHub repository](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/sqlvdi-linux).

## User permissions setup

On Linux, POSIX primitives are owned by the user who creates them and their default group. For objects that [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] creates, the default owners are the `mssql` user and the `mssql` group. To share these objects between [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] and the VDI client, use one of the following methods:

1. Run the VDI client as the `mssql` user.

   Run the following command to switch to the `mssql` user:

   ```bash
   sudo su mssql
   ```

1. Add the `mssql` user to the `vdiuser` group, and add the `vdiuser` user to the `mssql` group.

   Run the following commands:

   ```bash
   sudo useradd vdiuser
   sudo usermod -a -G mssql vdiuser
   sudo usermod -a -G vdiuser mssql
   ```

   Restart the server to pick up new groups for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] and `vdiuser`.

## Client functions

Each client function description includes:

- Function purpose
- Function syntax
- Parameter list
- Return values
- Remarks

## ClientVirtualDeviceSet::Create

### Purpose

This function creates the virtual device set.

### Syntax

```cpp
int ClientVirtualDeviceSet::Create (
    char     *    name,    // name for the set
    VDConfig *    cfg      // configuration for the set
);
```

#### Parameters

| Argument | Explanation |
| --- | --- |
| `name` | Identifies the virtual device set. Follow the rules for names used by `CreateFileMapping()`. Use any character except backslash (`\`). This argument is a character string. Prefix the string with your product or company name and database name. |
| `cfg` | The configuration for the virtual device set. |

#### Return values

| Argument | Explanation |
| --- | --- |
| `NOERROR` | The function succeeded. |
| `VD_E_NOTSUPPORTED` | One or more fields in the configuration were invalid or unsupported. |
| `VD_E_PROTOCOL` | The virtual device set already exists. |

### Remarks

Call the `Create` method only once per `BACKUP` or `RESTORE` operation. After `Close` is called, the client can reuse the interface to create another virtual device set.

## ClientVirtualDeviceSet::GetConfiguration

### Purpose

Use this function to wait for the server to configure the virtual device set.

### Syntax

```cpp
int ClientVirtualDeviceSet::GetConfiguration (
    time_t        timeout,    // in milliseconds
    VDConfig *    cfg         // selected configuration
);
```

#### Parameters

| Argument | Explanation |
| --- | --- |
| `timeout` | The timeout value in milliseconds. Use `INFINITE` or any negative integer to prevent timeout. |
| `cfg` | Contains the server-selected configuration on successful execution. |

#### Return values

| Argument | Explanation |
| --- | --- |
| `NOERROR` | The configuration was returned. |
| `VD_E_ABORT` | `SignalAbort` was invoked. |
| `VD_E_TIMEOUT` | The function timed out. |

#### Remarks

This function blocks in an `Alertable` state. After successful invocation, the devices in the virtual device set might be opened.

## ClientVirtualDeviceSet::OpenDevice

### Purpose

This function opens one of the devices in the virtual device set.

### Syntax

```cpp
int ClientVirtualDeviceSet::OpenDevice (
    char                *     name,              // name for the set
    ClientVirtualDevice **    ppVirtualDevice    // returns interface to device
);
```

#### Parameters

| Argument | Explanation |
| --- | --- |
| `name` | Identifies the virtual device set. |
| `ppVirtualDevice` | When the function succeeds, a pointer to the virtual device is returned. This device is used for `GetCommand` and `CompleteCommand`. |

#### Return values

| Argument | Explanation |
| --- | --- |
| `NOERROR` | The function succeeded. |
| `VD_E_ABORT` | Abort was requested. |
| `VD_E_OPEN` | All devices are open. |
| `VD_E_PROTOCOL` | The set isn't in the initializing state or this particular device is already open. |
| `VD_E_INVALID` | The device name is invalid. It isn't one of the names known to comprise the set. |

### Remarks

The function might return `VD_E_OPEN` without a problem. The client might call `OpenDevice` in a loop until this code is returned.
If you configure more than one device, for example *n* devices, the virtual device set returns *n* unique device interfaces.

Use the `GetConfiguration` function to wait until the devices can be opened.

If this function doesn't succeed, it returns a null value through the `ppVirtualDevice`.

## ClientVirtualDevice::GetCommand

### Purpose

Use this function to get the next command queued to a device. When requested, this function waits for the next command.

### Syntax

```cpp
int ClientVirtualDevice::GetCommand (
    time_t           timeout,    // time-out in milliseconds
    VDC_Command**    ppCmd       // returns the next command
);
```

#### Parameters

| Argument | Explanation |
| --- | --- |
| `timeout` | The time to wait, in milliseconds. Use `INFINITE` or a negative value to wait indefinitely. Use `0` to poll for a command. If no command is currently available, `VD_E_TIMEOUT` is returned. If the timeout occurs, the client decides the next action. |
| `ppCmd` | Returns the address of a command to execute, when a command is successfully returned. The memory returned is read-only. When the command is completed, this pointer is passed to the `CompleteCommand` routine. |

#### Return values

| Argument | Explanation |
| --- | --- |
| `NOERROR` | A command was fetched. |
| `VD_E_CLOSE` | The server closed the device. |
| `VD_E_TIMEOUT` | No command was available and the timeout expired. |
| `VD_E_ABORT` | Either the client or the server used the `SignalAbort` to force a shutdown. |

### Remarks

When the function returns `VD_E_CLOSE`, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] closed the device. This action is part of the normal shutdown. After all devices are closed, the client invokes `ClientVirtualDeviceSet::Close` to close the virtual device set.
When this routine must block to wait for a command, the thread is left in an `Alertable` condition.

## ClientVirtualDevice::CompleteCommand

### Purpose

Use this function to notify [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] that a command is complete. Return completion information that's appropriate for the command.

### Syntax

```cpp
int ClientVirtualDevice::CompleteCommand (
    VDC_Command      pCmd,                // the command
    int              completionCode,      // completion code
    unsigned long    bytesTransferred,    // bytes transferred
    int64_t          position             // current position
);
```

#### Parameters

| Argument | Explanation |
| --- | --- |
| `pCmd` | The address of a command previously returned from `ClientVirtualDevice::GetCommand`. |
| `completionCode` | A status code that indicates the completion status. Return this parameter for all commands. The code should match the command being performed. Use `ERROR_SUCCESS` to denote a successfully executed command. For the complete list of possible codes, see the file `vdierror.h`. |
| `bytesTransferred` | The number of successfully transferred bytes. Return this value only for data transfer commands `Read` and `Write`. |
| `position` | A response to the `GetPosition` command only. |

#### Return values

| Argument | Explanation |
| --- | --- |
| `NOERROR` | The completion was correctly noted. |
| `VD_E_INVALID` | `pCmd` wasn't an active command. |
| `VD_E_ABORT` | `Abort` was signaled. |
| `VD_E_PROTOCOL` | The device isn't open. |

### Remarks

None

## ClientVirtualDeviceSet::SignalAbort

### Purpose

Use this function to signal that an abnormal termination should occur.

### Syntax

```cpp
int ClientVirtualDeviceSet::SignalAbort ();
```

#### Parameters

| Argument | Explanation |
| --- | --- |
| None | Not applicable |

#### Return values

| Argument | Explanation |
| --- | --- |
| `NOERROR` | The `Abort` notification was successfully posted. |

### Remarks

At any time, the client might choose to abort the `BACKUP` or `RESTORE` operation. This routine signals that all operations should cease. The state of the overall virtual device set enters an `Abnormally Terminated` state. No further commands are returned on any devices. All uncompleted commands are automatically completed, returning `ERROR_OPERATION_ABORTED` as a completion code. The client should call `ClientVirtualDeviceSet::Close` after it safely terminates any outstanding use of buffers provided to the client.

## ClientVirtualDeviceSet::Close

### Purpose

This function closes the virtual device set created by `ClientVirtualDeviceSet::Create`. It releases all resources associated with the virtual device set.

### Syntax

```cpp
int ClientVirtualDeviceSet::Close ();
```

#### Parameters

| Argument | Explanation |
| --- | --- |
| None | Not applicable |

#### Return values

| Argument | Explanation |
| --- | --- |
| `NOERROR` | The virtual device set was closed successfully. |
| `VD_E_PROTOCOL` | The virtual device set wasn't open, so no action took place. |
| `VD_E_OPEN` | Devices are still open. |

### Remarks

When the client calls `Close`, it declares that all resources used by the virtual device set should be released. The client must ensure that all activity involving data buffers and virtual devices is terminated before invoking `Close`. `Close` invalidates all virtual device interfaces returned by `OpenDevice`.

After `Close` returns, the client can call `Create` on the virtual device set interface. This call creates a new virtual device set for a subsequent `BACKUP` or `RESTORE` operation.

If `Close` is called when one or more virtual devices are still open, `VD_E_OPEN` is returned. In this case, `SignalAbort` is triggered internally to ensure a proper shutdown if possible. VDI resources are released. The client should wait for a `VD_E_CLOSE` indication on each device before invoking `ClientVirtualDeviceSet::Close`. If the client knows that the virtual device set is already in an `Abnormally Terminated` state, it shouldn't expect a `VD_E_CLOSE` indication from `GetCommand`, and might invoke `ClientVirtualDeviceSet::Close` as soon as activity on the shared buffers is terminated.

## ClientVirtualDeviceSet::OpenInSecondary

### Purpose

This function opens the virtual device set in a secondary client. The primary client must already use `Create` and `GetConfiguration` to set up the virtual device set.

### Syntax

```cpp
int ClientVirtualDeviceSet::OpenInSecondary (
    char *    setName    // name of the set
);
```

#### Parameters

| Argument | Explanation |
| --- | --- |
| `setName` | This name identifies the set. It's case-sensitive and must match the name that the primary client passes to `ClientVirtualDeviceSet::Create`. |

#### Return values

| Argument | Explanation |
| --- | --- |
| `NOERROR` | The function succeeded. |
| `VD_E_PROTOCOL` | The virtual device set doesn't exist, is already open on this client, or isn't ready to accept open requests from secondary clients. |
| `VD_E_ABORT` | The operation is being aborted. |

> [!NOTE]  
> When using a multiple process model, the primary client is responsible for detecting normal and abnormal termination of secondary clients.

## ClientVirtualDeviceSet::GetBufferHandle

### Purpose

Some applications might require more than one process to operate on the buffers returned by `ClientVirtualDevice::GetCommand`. In such cases, the process that receives the command can use `GetBufferHandle` to obtain a process-independent handle that identifies the buffer. Communicate this handle to any other process that also has the same virtual device set open. That process uses `ClientVirtualDeviceSet::MapBufferHandle` to obtain the address of the buffer. The address is likely different than in its partner because each process might map buffers at different addresses.

### Syntax

```cpp
int ClientVirtualDeviceSet::GetBufferHandle (
    uint8_t*         pBuffer,         // in: buffer address
    unsigned int*    pBufferHandle    // out: buffer handle
);
```

#### Parameters

| Argument | Explanation |
| --- | --- |
| `pBuffer` | The address of a buffer that a `Read` or `Write` command returns. |
| `pBufferHandle` | A unique identifier for the buffer. |

#### Return values

| Argument | Explanation |
| --- | --- |
| `NOERROR` | The function succeeded. |
| `VD_E_PROTOCOL` | The virtual device set isn't currently open. |
| `VD_E_INVALID` | The `pBuffer` isn't a valid address. |

### Remarks

The process that invokes `GetBufferHandle` must invoke `ClientVirtualDevice::CompleteCommand` when the data transfer completes.

## ClientVirtualDeviceSet::MapBufferHandle

### Purpose

Use this function to get a valid buffer address from a buffer handle that another process provides.

### Syntax

```cpp
int ClientVirtualDeviceSet::MapBufferHandle (
    int          dwBuffer,    // in: buffer handle
    uint8_t**    ppBuffer     // out: buffer address
);
```

#### Parameters

| Argument | Explanation |
| --- | --- |
| `dwBuffer` | The handle that `ClientVirtualDeviceSet::GetBufferHandle` returns. |
| `ppBuffer` | The address of the buffer that's valid in the current process. |

#### Return values

| Argument | Explanation |
| --- | --- |
| `NOERROR` | The function succeeded. |
| `VD_E_PROTOCOL` | The virtual device set isn't currently open. |
| `VD_E_INVALID` | The `ppBuffer` is an invalid handle. |

### Remarks

Make sure you communicate the handles correctly. Handles are local to a single virtual device set. The partner processes that share a handle must ensure they use buffer handles only within the scope of the virtual device set from which the buffer was originally obtained.
