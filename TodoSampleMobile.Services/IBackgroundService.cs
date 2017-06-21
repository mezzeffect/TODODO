namespace TodoSampleMobile.Services
{
    public interface IBackgroundService
    {
        int RegisterForBackground();
        void EndBackgroundTask(int taskId);
    }
}
