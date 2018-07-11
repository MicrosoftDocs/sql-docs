public static void executeStoredProcedure(Connection con) {
   try {
      CallableStatement cstmt = con.prepareCall("{? = call dbo.CheckContactCity(?)}");
      cstmt.registerOutParameter(1, java.sql.Types.INTEGER);
      cstmt.setString(2, "Atlanta");
      cstmt.execute();
      System.out.println("RETURN STATUS: " + cstmt.getInt(1));
      cstmt.close();
   }
   catch (Exception e) {
      e.printStackTrace();
   }
}