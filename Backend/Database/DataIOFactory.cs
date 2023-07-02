class DataIOFactory {
    public static IDatabaseIO DatabaseIOCreate() {
        string gcpProject = Environment.GetEnvironmentVariable("GCP_PROJECT");
        bool isRunningOnGCP = !string.IsNullOrEmpty(gcpProject);
        return isRunningOnGCP ? new GCPDatabaseIO() : new DatabaseIO();
    }
}