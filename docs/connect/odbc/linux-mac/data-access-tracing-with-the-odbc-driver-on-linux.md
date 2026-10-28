---
title: Data Access Tracing on Linux and macOS
description: Enable unixODBC Driver Manager tracing for an ODBC application on Linux or macOS.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: 10/21/2026
ms.service: sql
ms.subservice: connectivity
ms.custom: linux-related-content
ms.topic: how-to
ai-usage: ai-assisted
helpviewer_keywords:
  - "data access tracing"
  - "tracing"
---
# Data access tracing on Linux and macOS

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This article shows you how to configure the unixODBC Driver Manager trace on Linux and macOS. The trace records ODBC API calls that you make through the Driver Manager. It doesn't trace the Microsoft ODBC Driver's internal events. For the difference between diagnostic records and tracing layers, see [ODBC diagnostics](../diagnostics.md).

The `[ODBC]` section of `odbcinst.ini` controls tracing for applications that use the unixODBC system configuration. Set `Trace=Yes` and specify an absolute path for `TraceFile`:

```ini
[ODBC]
Trace=Yes
TracePid=No
TraceFile=/home/myappuser/odbc-trace.ABC123/odbc.log
```

The path is illustrative; use the private absolute path generated in the following procedure.

Keep the driver registration entries that your application requires in this `odbcinst.ini` file. The `[ODBC]` section is global to the configuration. Adding `Trace=Yes` to a driver-specific section doesn't scope the unixODBC Driver Manager trace to that driver.

The default trace-file path isn't scoped to your application. Use a private directory and an explicit trace-file path instead. Tracing can affect performance, and the output can contain application data. Restrict access to the file and capture only the calls you need.

Use a unixODBC build that includes the upstream [diagnostic trace buffer fix](https://github.com/lurcher/unixODBC/commit/ad6fcd536b2c8940dd501bf094b756bb89a52e53). Vendor packages can include a backport; check the package's patches instead of relying on the version number alone. Without the fix, the application can abort in `SQLGetDiagRec` while the manager formats a native error for tracing.

## Trace one application launch

Open a dedicated Bash shell, then run the shell commands in this procedure in that same terminal session. The first command enables fail-fast handling, so a failed directory creation, permission change, or configuration copy stops before the application launch. Keep the shell open for the later steps and type `exit` after cleanup.

1. Run `odbcinst -j` and note the paths for the system driver and system data source files. Create private configuration and trace directories, then copy the system configuration files. Replace the example file paths with those reported on your system:

   ```bash
   set -eu
   config_dir=$(mktemp -d "$HOME/odbc-trace-config.XXXXXX")
   trace_dir=$(mktemp -d "$HOME/odbc-trace.XXXXXX")
   if [ "$(printf '%s' "$trace_dir/odbc.log" | wc -c)" -gt 62 ]; then
       printf 'Use a shorter parent path for the trace directory (log path exceeds 62 bytes).\n' >&2
       rmdir "$trace_dir" "$config_dir"
       exit 1
   fi
   driver_file=/etc/odbcinst.ini
   data_sources_file=/etc/odbc.ini
   chmod 700 "$config_dir" "$trace_dir"
   cp "$driver_file" "$config_dir/odbcinst.ini"
   if [ -f "$data_sources_file" ]; then
       cp "$data_sources_file" "$config_dir/odbc.ini"
   else
       printf '[ODBC Data Sources]\n' > "$config_dir/odbc.ini"
   fi
   chmod 600 "$config_dir/odbcinst.ini" "$config_dir/odbc.ini"
   printf '%s\n' "$trace_dir/odbc.log"
   ```

   The pinned [unixODBC trace configuration implementation](https://github.com/lurcher/unixODBC/blob/032cc6b17c91feffdfbe3ba81b2c57b69190a557/DriverManager/__handles.c) supplies a 64-byte buffer to the [registration profile reader](https://github.com/lurcher/unixODBC/blob/032cc6b17c91feffdfbe3ba81b2c57b69190a557/odbcinst/_SQLGetInstalledDrivers.c). That reader safely copies a path of up to 62 bytes and its null terminator. For longer paths, its truncation branch doesn't append a null terminator. Checks with unixODBC 2.3.14 on Linux and macOS confirmed correct trace creation at 62 bytes. The guard counts bytes and stops before launch if the generated path is too long. If it stops, use a shorter writable parent path in the `trace_dir` command, keep `mktemp -d`, and restart the procedure.

   Keep all driver registration entries that the application needs in the copied `odbcinst.ini`. If the application uses a system data source name (DSN), keep its definition in the copied `odbc.ini`. If its `Driver` value is a registration name, keep that matching definition in the copied `odbcinst.ini`. If the value is an absolute library path, confirm that the library exists and is accessible at that path. At the pinned unixODBC revision, a value beginning with `/` is used as the library path; other values are resolved through a driver registration. For more information, see the unixODBC [odbc.ini manual](https://github.com/lurcher/unixODBC/blob/032cc6b17c91feffdfbe3ba81b2c57b69190a557/man/odbc.ini.5) and the pinned [driver lookup implementation](https://github.com/lurcher/unixODBC/blob/032cc6b17c91feffdfbe3ba81b2c57b69190a557/DriverManager/__connection.c). The commands leave the user DSN configuration in place.

1. In the private `odbcinst.ini`, add or update the global `[ODBC]` section. Use the absolute path printed by the previous command for `TraceFile`; don't create a second `[ODBC]` section:

   ```ini
   [ODBC]
   Trace=Yes
   TracePid=No
   TraceFile=/home/myappuser/odbc-trace.ABC123/odbc.log
   ```

   Replace the example path with the exact path printed in the previous step. Set `TracePid=No` even if the copied configuration enables it, so the trace uses the single file shown here.

1. Launch the application with the private system configuration directory:

   ```bash
   if (umask 077; ODBCSYSINI="$config_dir" ODBCINSTINI=odbcinst.ini \
       ODBCSEARCH=ODBC_BOTH_DSN ./your-app); then
       :
   else
       app_status=$?
       printf 'Application exited with status %s\n' "$app_status" >&2
   fi
   ```

   The trace file should contain ODBC API calls and their return codes. The conditional keeps the shell open for cleanup even if the application exits with an error. The unixODBC Driver Manager reads its system configuration directory from `ODBCSYSINI`. Set `ODBCINSTINI=odbcinst.ini` for this child process so an inherited registration-filename override doesn't bypass the copied file. For the filename lookup, see the pinned [unixODBC configuration implementation](https://github.com/lurcher/unixODBC/blob/032cc6b17c91feffdfbe3ba81b2c57b69190a557/odbcinst/_odbcinst_SystemINI.c).

   `ODBCSEARCH=ODBC_BOTH_DSN` overrides an inherited user-only or system-only DSN search mode for this launch. These settings leave the existing user DSN file in place. unixODBC searches both system and user DSN files, and the user DSN takes precedence when the same name exists in both. Check for a duplicate user DSN if the application appears to use different settings than the copied system DSN. If the application intentionally requires a restricted search mode, preserve that mode instead and check that its selected DSN file contains the required definition. For more information, see the pinned unixODBC [search-mode implementation](https://github.com/lurcher/unixODBC/blob/032cc6b17c91feffdfbe3ba81b2c57b69190a557/odbcinst/SQLSetConfigMode.c), [isql manual](https://github.com/lurcher/unixODBC/blob/032cc6b17c91feffdfbe3ba81b2c57b69190a557/man/isql.1), and [odbc.ini manual](https://github.com/lurcher/unixODBC/blob/032cc6b17c91feffdfbe3ba81b2c57b69190a557/man/odbc.ini.5).

When the reproduction ends, exit the application to release its ODBC environment handles. Set `Trace=No` or remove the `[ODBC]` tracing settings before another launch. Restrict access to the log before you inspect or share it:

```bash
if [ -f "$trace_dir/odbc.log" ]; then
    chmod 600 "$trace_dir/odbc.log"
else
    printf 'No trace file was created. Check that the application started and allocated an ODBC environment.\n' >&2
fi
```

An application that fails to start or exits before allocating an ODBC environment might not create a trace file. The check reports the missing file and keeps cleanup reachable in the dedicated shell.

Redact sensitive content before sharing the trace. Delete the log when you no longer need it. Remove the private configuration files and directories only if you created them for this trace and no longer need their contents.

The following commands remove only the files and directories created in this procedure:

```bash
rm -f "$trace_dir/odbc.log" "$config_dir/odbcinst.ini" "$config_dir/odbc.ini"
rmdir "$trace_dir" "$config_dir"
```

## Find the configuration files used by the Driver Manager

<a name="determine-which-file-the-driver-is-using"></a>

Run `odbcinst -j` to display the system and user configuration paths. Paths can vary by platform and unixODBC build. *User DSNs* are available to a specific user. *System DSNs* are available to users on the system, and managing them can require administrator permissions. When the same DSN name exists in both files, the user DSN takes precedence. For more information, see the [unixODBC documentation](https://www.unixodbc.org/doc/UserManual/).

## Related content

- [Programming Guidelines](programming-guidelines.md)
