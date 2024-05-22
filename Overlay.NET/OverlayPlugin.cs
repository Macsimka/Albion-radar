using Overlay.NET.Common;
using System;

namespace Overlay.NET
{
    /// <summary>
    ///     Abstract class that defines basic overlay operations and values.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public abstract class OverlayPlugin : IDisposable
    {
        /// <summary>
        ///     Gets or sets the target window that the overlay is to 'attach' to.
        /// </summary>
        /// <value>
        ///     The target window.
        /// </value>
        public IntPtr TargetWindowHandle { get; protected set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; protected set; }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose() { }

        /// <summary>
        ///     Enables this instance.
        /// </summary>
        public virtual void Enable() => IsEnabled = true;

        /// <summary>
        ///     Disables this instance.
        /// </summary>
        public virtual void Disable() => IsEnabled = false;

        /// <summary>
        ///     Initializes the specified target window handle.
        /// </summary>
        /// <param name="targetWindowHandle">The target window handle.</param>
        public virtual void Initialize(IntPtr targetWindowHandle) => TargetWindowHandle = targetWindowHandle;

        public bool IsActive() => Native.GetForegroundWindow() == TargetWindowHandle;

        /// <summary>
        ///     Updates this instance.
        /// </summary>
        public virtual void Update() { }
    }
}
