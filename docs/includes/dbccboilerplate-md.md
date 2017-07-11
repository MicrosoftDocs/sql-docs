Look for Hardware FailureRun hardware diagnostics and correct any problems. Also examine the Windows system and application logs and the SQL Server error log to see whether the error occurred as the result of hardware failure. Fix any hardware\-related problems that are contained in the logs.

If you have persistent data corruption problems, try to swap out different hardware components to isolate the problem. Check to make sure that the system does not have write\-caching enabled on the disk controller. If you suspect write\-caching to be the problem, contact your hardware vendor.

Finally, you might find it useful to switch to a new hardware system. This switch may include reformatting the disk drives and reinstalling the operating system.

Restore from BackupIf the problem is not hardware related and a known clean backup is available, restore the database from the backup.

Run DBCC CHECKDBIf no clean backup is available, run DBCC CHECKDB without a REPAIR clause to determine the extent of the corruption. DBCC CHECKDB will recommend a REPAIR clause to use. Then, run DBCC CHECKDB with the appropriate REPAIR clause to repair the corruption.

> **alert tag is not supported!!!!**
> **tr tag is not supported!!!!**
> **tr tag is not supported!!!!**

If running DBCC CHECKDB with one of the REPAIR clauses does not correct the problem, contact your primary support provider.

