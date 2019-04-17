---
title: "Recordset (Visual C++ Syntax Index with #import) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
dev_langs: 
  - "C++"
helpviewer_keywords: 
  - "Recordset collection [ADO], Visual C++ syntax index with #import"
ms.assetid: fe41da71-b607-4329-94da-60964b8efcdc
author: MightyPen
ms.author: genemi
manager: craigg
---
# Recordset (Visual C++ Syntax Index with #import)
## Methods  
  
```  
HRESULT AddNew( const _variant_t & FieldList = vtMissing,  
    const    _variant_t & Values = vtMissing );  
  
HRESULT Cancel( );  
  
HRESULT CancelBatch( enum AffectEnum AffectRecords );  
  
HRESULT CancelUpdate( );  
  
_RecordsetPtr Clone( enum LockTypeEnum LockType );  
  
HRESULT Close( );  
  
enum CompareEnum CompareBookmarks( const _variant_t  
    & Bookmark1, const     _variant_t & Bookmark2 );  
  
HRESULT Delete( enum AffectEnum AffectRecords );  
  
HRESULT Find( _bstr_t Criteria, long SkipRecords, enum  
    SearchDirectionEnum SearchDirection, const _variant_t & Start =  
    vtMissing );  
  
_variant_t GetRows( long Rows, const _variant_t & Start =  
    vtMissing, const _variant_t & Fields = vtMissing );  
  
_bstr_t GetString( enum  
    StringFormatEnum StringFormat, long NumRows,     _bstr_t   
    ColumnDelimeter, _bstr_t RowDelimeter, _bstr_t NullExpr );  
  
HRESULT Move( long NumRecords, const _variant_t & Start =   
    vtMissing );  
  
HRESULT MoveFirst( );  
HRESULT MoveLast( );  
HRESULT MoveNext( );  
HRESULT MovePrevious( );  
  
_RecordsetPtr NextRecordset( VARIANT * RecordsAffected );  
  
HRESULT Open( const _variant_t & Source, const _variant_t &  
    ActiveConnection, enum CursorTypeEnum CursorType, enum LockTypeEnum  
    LockType, long Options );  
  
HRESULT Requery( long Options );  
  
HRESULT Update( const _variant_t & Fields = vtMissing, const  
    _variant_t &     Values = vtMissing );  
  
HRESULT UpdateBatch( enum AffectEnum AffectRecords );  
  
HRESULT Resync( enum AffectEnum AffectRecords, enum  
    ResyncEnum     ResyncValues );  
  
HRESULT Save( const _variant_t & Destination, enum  
    PersistFormatEnum     PersistFormat );  
  
HRESULT Seek( const _variant_t & KeyValues, enum SeekEnum  
    SeekOption );  
  
VARIANT_BOOL Supports( enum CursorOptionEnum CursorOptions );  
```  
  
## Properties  
  
```  
enum PositionEnum GetAbsolutePage( );  
void PutAbsolutePage( enum PositionEnum pl );  
__declspec(property(get=GetAbsolutePage,put=PutAbsolutePage)) enum  
    PositionEnum AbsolutePage;  
  
enum PositionEnum GetAbsolutePosition( );  
void PutAbsolutePosition( enum PositionEnum pl );  
__declspec(property(get=GetAbsolutePosition,put=PutAbsolutePosition))  
    enum PositionEnum AbsolutePosition;  
  
IDispatchPtr GetActiveCommand( );  
__declspec(property(get=GetActiveCommand)) IDispatchPtr ActiveCommand;  
  
void PutRefActiveConnection( IDispatch * pvar );  
void PutActiveConnection( const _variant_t & pvar );  
_variant_t GetActiveConnection( );  
  
enum CursorLocationEnum GetCursorLocation( );  
void PutCursorLocation( enum CursorLocationEnum plCursorLoc );  
__declspec(property(get=GetCursorLocation,put=PutCursorLocation)) enum  
    CursorLocationEnum CursorLocation;  
  
VARIANT_BOOL GetBOF( );  
__declspec(property(get=GetBOF)) VARIANT_BOOL BOF;  
  
VARIANT_BOOL GetEndOfFile( ); // Actually, GetEOF. Renamed in #import.  
__declspec(property(get=GetEndOfFile)) VARIANT_BOOL EndOfFile;  
  
_variant_t GetBookmark( );  
void PutBookmark( const _variant_t & pvBookmark );  
__declspec(property(get=GetBookmark,put=PutBookmark)) _variant_t  
    Bookmark;  
  
long GetCacheSize( );  
void PutCacheSize( long pl );  
__declspec(property(get=GetCacheSize,put=PutCacheSize)) long  
    CacheSize;  
  
enum CursorTypeEnum GetCursorType( );  
void PutCursorType( enum CursorTypeEnum plCursorType );  
__declspec(property(get=GetCursorType,put=PutCursorType)) enum  
    CursorTypeEnum CursorType;  
  
_bstr_t GetDataMember( );  
void PutDataMember( _bstr_t pbstrDataMember );  
__declspec(property(get=GetDataMember,put=PutDataMember)) _bstr_t  
    DataMember;  
  
IUnknownPtr GetDataSource( );  
void PutRefDataSource( IUnknown * ppunkDataSource );  
__declspec(property(get=GetDataSource,put=PutRefDataSource)) IUnknownPtr  
    DataSource;  
  
enum EditModeEnum GetEditMode( );  
__declspec(property(get=GetEditMode)) enum EditModeEnum EditMode;  
  
FieldsPtr GetFields( );  
__declspec(property(get=GetFields)) FieldsPtr Fields;  
  
_variant_t GetFilter( );  
void PutFilter( const _variant_t & Criteria );  
__declspec(property(get=GetFilter,put=PutFilter)) _variant_t Filter;  
  
_bstr_t GetIndex( );  
void PutIndex( _bstr_t pbstrIndex );  
__declspec(property(get=GetIndex,put=PutIndex)) _bstr_t Index;  
  
enum LockTypeEnum GetLockType( );  
void PutLockType( enum LockTypeEnum plLockType );  
__declspec(property(get=GetLockType,put=PutLockType)) enum LockTypeEnum  
    LockType;  
  
enum MarshalOptionsEnum GetMarshalOptions( );  
void PutMarshalOptions( enum MarshalOptionsEnum peMarshal );  
__declspec(property(get=GetMarshalOptions,put=PutMarshalOptions)) enum  
    MarshalOptionsEnum MarshalOptions;  
  
long GetMaxRecords( );  
void PutMaxRecords( long plMaxRecords );  
__declspec(property(get=GetMaxRecords,put=PutMaxRecords)) long  
    MaxRecords;  
  
long GetPageCount( );  
__declspec(property(get=GetPageCount)) long PageCount;  
  
long GetPageSize( );  
void PutPageSize( long pl );  
__declspec(property(get=GetPageSize,put=PutPageSize)) long PageSize;  
  
long GetRecordCount( );  
__declspec(property(get=GetRecordCount)) long RecordCount;  
  
_bstr_t GetSort( );  
void PutSort( _bstr_t Criteria );  
__declspec(property(get=GetSort,put=PutSort)) _bstr_t Sort;  
  
void PutRefSource( IDispatch * pvSource );  
void PutSource( _bstr_t pvSource );  
_variant_t GetSource( );  
  
long GetState( );  
__declspec(property(get=GetState)) long State;  
  
long GetStatus( );  
__declspec(property(get=GetStatus)) long Status;  
  
VARIANT_BOOL GetStayInSync( );  
void PutStayInSync( VARIANT_BOOL pbStayInSync );  
__declspec(property(get=GetStayInSync,put=PutStayInSync)) VARIANT_BOOL  
    StayInSync;  
```  
  
## See Also  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)
