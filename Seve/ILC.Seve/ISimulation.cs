namespace ILC.Seve
{
    /// <summary>
    /// A simulation runner.  This is responsible for managing all of the
    /// low-level stuff, including interfacing with the Algorithm
    /// namespace.
    /// </summary>
    public interface ISimulation
    {
        /// <summary>
        /// Run the simulation.
        /// </summary>
        void RunSimulation();
    }
}
