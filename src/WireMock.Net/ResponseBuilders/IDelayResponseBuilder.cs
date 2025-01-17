﻿using System;

namespace WireMock.ResponseBuilders
{
    /// <summary>
    /// The DelayResponseBuilder interface.
    /// </summary>
    public interface IDelayResponseBuilder : ICallbackResponseBuilder
    {
        /// <summary>
        /// The with delay.
        /// </summary>
        /// <param name="delay">The TimeSpan to delay.</param>
        /// <returns>The <see cref="IResponseBuilder"/>.</returns>
        IResponseBuilder WithDelay(TimeSpan delay);

        /// <summary>
        /// The with delay.
        /// </summary>
        /// <param name="milliseconds">The milliseconds to delay.</param>
        /// <returns>The <see cref="IResponseBuilder"/>.</returns>
        IResponseBuilder WithDelay(int milliseconds);

        /// <summary>
        /// Introduce random delay
        /// </summary>
        /// <param name="minimumMilliseconds">Minimum milliseconds to delay</param>
        /// <param name="maximumMilliseconds">Maximum milliseconds to delay</param>
        /// <returns>The <see cref="IResponseBuilder"/>.</returns>
        IResponseBuilder WithRandomDelay(int minimumMilliseconds = 0, int maximumMilliseconds = 60_000);
    }
}