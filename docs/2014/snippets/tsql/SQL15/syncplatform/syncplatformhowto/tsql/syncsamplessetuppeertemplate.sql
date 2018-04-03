--<snippetOCSv3_SQL_SyncSamplesSetupPeerTemplate>
--
-- ========================================================================================================================================================================
--  Template for Creating Stored Procedures for DbSyncAdapter Commands
-- ========================================================================================================================================================================
--

use [database]
go

if object_id(N'dbo.sp_[basetable]_selectchanges', 'P') is not null
	drop procedure dbo.sp_[basetable]_selectchanges

if object_id(N'dbo.sp_[basetable]_applyinsert', 'P') is not null
	drop procedure dbo.sp_[basetable]_applyinsert
	
if object_id(N'dbo.sp_[basetable]_insertmetadata', 'P') is not null
	drop procedure dbo.sp_[basetable]_insertmetadata	

if object_id(N'dbo.sp_[basetable]_applyupdate', 'P') is not null
	drop procedure dbo.sp_[basetable]_applyupdate

if object_id(N'dbo.sp_[basetable]_updatemetadata', 'P') is not null
	drop procedure dbo.sp_[basetable]_updatemetadata

if object_id(N'dbo.sp_[basetable]_applydelete', 'P') is not null
	drop procedure dbo.sp_[basetable]_applydelete

if object_id(N'dbo.sp_[basetable]_deletemetadata', 'P') is not null
	drop procedure dbo.sp_[basetable]_deletemetadata

if object_id(N'dbo.sp_[basetable]_selectrow', 'P') is not null
	drop procedure dbo.sp_[basetable]_selectrow

if object_id(N'dbo.sp_[basetable]_selecttombstones', 'P') is not null
	drop procedure dbo.sp_[basetable]_selecttombstones

if object_id (N'dbo.sp_select_shared_scopes', 'P') is not null
	drop procedure dbo.sp_select_shared_scopes
go

go
--
--  ***********************************************
--     Select Incremental Changes Proc for [basetable]
--  ***********************************************
--
create procedure dbo.sp_[basetable]_selectchanges (
    @sync_min_timestamp bigint,
    @sync_metadata_only int,
    @sync_scope_local_id int
)
as
begin
    select  tt.pkcols,
            bt.nonPkCols, 
            tt.sync_row_is_tombstone,
            tt.local_update_peer_timestamp as sync_row_timestamp, 
            case when (tt.update_scope_local_id is null or tt.update_scope_local_id <> @sync_scope_local_id) 
                 then case when (tt.restore_timestamp is null) then tt.local_update_peer_timestamp else tt.restore_timestamp end else tt.scope_update_peer_timestamp end as sync_update_peer_timestamp,
            case when (tt.update_scope_local_id is null or tt.update_scope_local_id <> @sync_scope_local_id) 
                 then tt.local_update_peer_key else tt.scope_update_peer_key end as sync_update_peer_key,
            case when (tt.create_scope_local_id is null or tt.create_scope_local_id <> @sync_scope_local_id) 
                 then tt.local_create_peer_timestamp else tt.scope_create_peer_timestamp end as sync_create_peer_timestamp,
            case when (tt.create_scope_local_id is null or tt.create_scope_local_id <> @sync_scope_local_id) 
                 then tt.local_create_peer_key else tt.scope_create_peer_key end as sync_create_peer_key
    from [basetable] bt right join [trackingtable] tt on [bt.pk = tt.pk]
    where tt.local_update_peer_timestamp > @sync_min_timestamp
end
go


--
--  ***********************************************
--     Insert Procs for [basetable]
--  ***********************************************
--
create procedure dbo.sp_[basetable]_applyinsert (                        
        @order_id int,
        @order_date datetime,
        @sync_row_count int out)        
as
    if not exists (select * from [trackingtable] tt where [pkcol = @pkcol])
        insert into [basetable] ([allcols]) 
            values ([@allcols])
    set @sync_row_count = @@rowcount
go


create procedure dbo.sp_[basetable]_insertmetadata (
        [@pkcols_with_datatype],
        @sync_scope_local_id int,
        @sync_row_is_tombstone int,
        @sync_create_peer_key int ,
        @sync_create_peer_timestamp bigint,                 
        @sync_update_peer_key int ,
        @sync_update_peer_timestamp timestamp,                              
        @sync_check_concurrency int,    
        @sync_row_timestamp timestamp,  
        @sync_row_count int out)        
as  
begin
update [trackingtable] set 
    [create_scope_local_id] = @sync_scope_local_id, 
    [scope_create_peer_key] = @sync_create_peer_key,
    [scope_create_peer_timestamp] = @sync_create_peer_timestamp,
    [local_create_peer_key] = 0,
    [local_create_peer_timestamp] = @@DBTS+1,
    [update_scope_local_id] = @sync_scope_local_id,
    [scope_update_peer_key] = @sync_update_peer_key,
    [scope_update_peer_timestamp] = @sync_update_peer_timestamp,
    [local_update_peer_key] = 0,
    [restore_timestamp] = NULL,
    [sync_row_is_tombstone] = @sync_row_is_tombstone 
    where [pkcol = @pkcol]
    and (@sync_check_concurrency = 0 or local_update_peer_timestamp = @sync_row_timestamp) 
set @sync_row_count = @@ROWCOUNT
end
go


create procedure dbo.sp_[basetable]_insertmetadata (
        [@pkcols_with_datatype],
        @sync_scope_local_id int,
        @sync_row_is_tombstone int,
        @sync_create_peer_key int ,
        @sync_create_peer_timestamp bigint,                 
        @sync_update_peer_key int ,
        @sync_update_peer_timestamp timestamp,                              
        @sync_check_concurrency int,    
        @sync_row_timestamp timestamp,  
        @sync_row_count int out)        
as  
begin
	update [trackingtable] set 
    [create_scope_local_id] = @sync_scope_local_id, 
    [scope_create_peer_key] = @sync_create_peer_key,
    [scope_create_peer_timestamp] = @sync_create_peer_timestamp,
    [local_create_peer_key] = 0,
    [local_create_peer_timestamp] = @@DBTS+1,
    [update_scope_local_id] = @sync_scope_local_id,
    [scope_update_peer_key] = @sync_update_peer_key,
    [scope_update_peer_timestamp] = @sync_update_peer_timestamp,
    [local_update_peer_key] = 0,
    [restore_timestamp] = NULL,
    [sync_row_is_tombstone] = @sync_row_is_tombstone 
    where [pkcol = @pkcol]
    and (@sync_check_concurrency = 0 or local_update_peer_timestamp = @sync_row_timestamp) 
    
	set @sync_row_count = @@ROWCOUNT
	
	if (@sync_row_count = 0 )
	begin
		-- inserting metadata for row if it does not already exist
		-- this can happen if a node sees a delete for a row it never had, we insert only metadata
		-- for the row in that case
		insert into [trackingtable] (	
		[pkcols],
		[create_scope_local_id], 
		[scope_create_peer_key],
		[scope_create_peer_timestamp],
		[local_create_peer_key],
		[local_create_peer_timestamp],
		[update_scope_local_id],
		[scope_update_peer_key],
		[scope_update_peer_timestamp],
		[local_update_peer_key],
		[restore_timestamp],
		[sync_row_is_tombstone] )values (
		[@pkcols],
		@sync_scope_local_id, 
		@sync_create_peer_key,
		@sync_create_peer_timestamp,
		0,
		@@DBTS+1,
		@sync_scope_local_id,
		@sync_update_peer_key,
		@sync_update_peer_timestamp,
		0,
		NULL,
		@sync_row_is_tombstone)
		set @sync_row_count = @@ROWCOUNT
	end
end
go


--
--  ***********************************************
--     Update Procs for [basetable]
--  ***********************************************
--

create procedure dbo.sp_[basetable]_applyupdate (                                    
        [@allcols_with_datatype],
        @sync_force_write int,
        @sync_min_timestamp bigint ,                                
        @sync_row_count int out)        
as      
    update [basetable]
    set [set_allcols_except_PK] from [basetable] bt join [trackingtable] tt on [bt.pk = tt.pk]
    where (tt.local_update_peer_timestamp <= @sync_min_timestamp or @sync_force_write = 1)        
        and [tt.pk = @pk]  
    set @sync_row_count = @@rowcount                        
go


create procedure dbo.sp_[basetable]_updatemetadata (
        [@pkcols_with_datatype],
        @sync_scope_local_id int,
        @sync_row_is_tombstone int,
        @sync_create_peer_key int ,
        @sync_create_peer_timestamp bigint,                 
        @sync_update_peer_key int ,
        @sync_update_peer_timestamp timestamp,                      
        @sync_row_timestamp timestamp,
        @sync_check_concurrency int,        
        @sync_row_count int out)        
as          

    declare @was_tombstone int 
    select @was_tombstone = sync_row_is_tombstone from [trackingtable] 
    where [order_id] = @order_id 
    if (@was_tombstone is not null and @was_tombstone=1 and @sync_row_is_tombstone=0) 
        update [trackingtable] set 
            [update_scope_local_id] = @sync_scope_local_id, 
            [scope_update_peer_key] = @sync_update_peer_key,
            [scope_update_peer_timestamp] = @sync_update_peer_timestamp,
            [local_update_peer_key] = 0,
            [restore_timestamp] = NULL,
            [create_scope_local_id] = @sync_scope_local_id, 
            [scope_create_peer_key] = @sync_create_peer_key, 
            [scope_create_peer_timestamp] =  @sync_create_peer_timestamp, 
            [sync_row_is_tombstone] = @sync_row_is_tombstone 
        where [pkcol = @pkcol]
        and (@sync_check_concurrency = 0 or local_update_peer_timestamp = @sync_row_timestamp) 
    else 
        update [trackingtable] set 
            [update_scope_local_id] = @sync_scope_local_id, 
            [scope_update_peer_key] = @sync_update_peer_key,
            [scope_update_peer_timestamp] = @sync_update_peer_timestamp,
            [local_update_peer_key] = 0,
            [restore_timestamp] = NULL,
            [sync_row_is_tombstone] = @sync_row_is_tombstone 
        where [pkcol = @pkcol]
        and (@sync_check_concurrency = 0 or local_update_peer_timestamp = @sync_row_timestamp) 
    set @sync_row_count = @@rowcount
go  

--
--  ***********************************************
--     Delete Procs for [basetable]
--  ***********************************************
--

create procedure dbo.sp_[basetable]_applydelete(
    [@pkcols_with_datatype],
    @sync_min_timestamp bigint ,            
    @sync_force_write int,
    @sync_row_count int out)     
as  
    delete [basetable] from [basetable] bt join [trackingtable] tt on [bt.pk = tt.pk]
    where (tt.local_update_peer_timestamp <= @sync_min_timestamp or @sync_force_write = 1)
        and [tt.pk = @pk]            
    set @sync_row_count = @@rowcount              
go

create procedure dbo.sp_[basetable]_deletemetadata(
    [@pkcols_with_datatype],   
    @sync_row_timestamp timestamp,  
    @sync_check_concurrency int,    
    @sync_row_count int out)    
as    
    -- delete metadata only
    delete tt
    from [trackingtable] tt
    where [tt.pk = @pk] and (@sync_check_concurrency = 0 or local_update_peer_timestamp = @sync_row_timestamp)
    set @sync_row_count = @@rowcount            
go


--
--  ***********************************************
--     Get conflicting row proc
--  ***********************************************
--

create procedure dbo.sp_[basetable]_selectrow
        [@pkcols_with_datatype],
        @sync_scope_local_id int
as

select      [tt.pk],
            [bt.nonPkCols], 
            tt.sync_row_is_tombstone,
            tt.local_update_peer_timestamp as sync_row_timestamp, 
            case when (tt.update_scope_local_id is null or tt.update_scope_local_id <> @sync_scope_local_id) 
                 then case when (tt.restore_timestamp is null) then tt.local_update_peer_timestamp else tt.restore_timestamp end else tt.scope_update_peer_timestamp end as sync_update_peer_timestamp,
            case when (tt.update_scope_local_id is null or tt.update_scope_local_id <> @sync_scope_local_id) 
                 then tt.local_update_peer_key else tt.scope_update_peer_key end as sync_update_peer_key,
            case when (tt.create_scope_local_id is null or tt.create_scope_local_id <> @sync_scope_local_id) 
                 then tt.local_create_peer_timestamp else tt.scope_create_peer_timestamp end as sync_create_peer_timestamp,
            case when (tt.create_scope_local_id is null or tt.create_scope_local_id <> @sync_scope_local_id) 
                 then tt.local_create_peer_key else tt.scope_create_peer_key end as sync_create_peer_key
    from [basetable] bt right join [trackingtable] tt on [bt.pk = tt.pk]    
    where [tt.pk = @pk]
go
--
--  ***********************************************
--     Get tombstones for cleanup proc
--  ***********************************************
--


create procedure dbo.sp_[basetable]_selecttombstones     
	@tombstone_aging_in_hours int,
	@sync_scope_local_id int
	
as
	select	[pkcols],
	local_update_peer_timestamp as sync_row_timestamp,  
	case when (update_scope_local_id is null or update_scope_local_id <> @sync_scope_local_id) 
        then case when (restore_timestamp is null) then local_update_peer_timestamp else restore_timestamp end else scope_update_peer_timestamp end as sync_update_peer_timestamp,
        case when (update_scope_local_id is null or update_scope_local_id <> @sync_scope_local_id) 
        then local_update_peer_key else scope_update_peer_key end as sync_update_peer_key,
        case when (create_scope_local_id is null or create_scope_local_id <> @sync_scope_local_id) 
        then local_create_peer_timestamp else scope_create_peer_timestamp end as sync_create_peer_timestamp,
        case when (create_scope_local_id is null or create_scope_local_id <> @sync_scope_local_id) 
        then local_create_peer_key else scope_create_peer_key end as sync_create_peer_key			
	from [trackingtable] 
	where sync_row_is_tombstone=1 
	and DATEDIFF(hh, last_change_datetime, GetDate()) > @tombstone_aging_in_hours go

create procedure dbo.sp_select_shared_scopes
		@sync_scope_name nvarchar(100)		
as
	select	scopeTableMap2.table_name as sync_table_name, 
			scopeTableMap2.scope_name as sync_shared_scope_name
	from [ScopeTableMap] scopeTableMap1 join [ScopeTableMap] scopeTableMap2
	on scopeTableMap1.table_name = scopeTableMap2.table_name
	and scopeTableMap1.scope_name = @sync_scope_name
	where scopeTableMap2.scope_name <> @sync_scope_name
go


--</snippetOCSv3_SQL_SyncSamplesSetupPeerTemplate>