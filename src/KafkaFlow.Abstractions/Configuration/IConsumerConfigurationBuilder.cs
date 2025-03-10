namespace KafkaFlow.Configuration
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used to build the consumer configuration
    /// </summary>
    public interface IConsumerConfigurationBuilder
    {
        /// <summary>
        /// Gets the dependency injection configurator
        /// </summary>
        IDependencyConfigurator DependencyConfigurator { get; }

        /// <summary>
        /// Sets the topic that will be used to read the messages
        /// </summary>
        /// <param name="topicName">Topic name</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder Topic(string topicName);

        /// <summary>
        /// Sets the topics that will be used to read the messages
        /// </summary>
        /// <param name="topicNames">Topic names</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder Topics(IEnumerable<string> topicNames);

        /// <summary>
        /// Sets the topics that will be used to read the messages
        /// </summary>
        /// <param name="topicNames">Topic names</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder Topics(params string[] topicNames);

        /// <summary>
        /// Sets a unique name for the consumer
        /// </summary>
        /// <param name="name">A unique name</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithName(string name);

        /// <summary>
        /// Sets the consumer as readonly, that means this consumer can not be managed and it will not send telemetry data
        /// </summary>
        /// <returns></returns>
        IConsumerConfigurationBuilder DisableManagement();

        /// <summary>
        /// Sets the group id used by the consumer
        /// </summary>
        /// <param name="groupId">The consumer id value</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithGroupId(string groupId);

        /// <summary>
        /// Sets the initial offset strategy used by new consumer groups.
        /// If your consumer group (set by method <see cref="WithGroupId(string)"/>) has no offset stored in Kafka, this configuration will be used
        /// </summary>
        /// <param name="autoOffsetReset">The <see cref="AutoOffsetReset"/> enum value</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithAutoOffsetReset(AutoOffsetReset autoOffsetReset);

        /// <summary>
        /// Sets the interval used by the framework to commit the stored offsets in Kafka
        /// </summary>
        /// <param name="autoCommitIntervalMs">The interval in milliseconds</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithAutoCommitIntervalMs(int autoCommitIntervalMs);

        /// <summary>
        /// Sets the max interval between message consumption, if this time exceeds the consumer is considered failed and Kafka will revoke the assigned partitions
        /// </summary>
        /// <param name="maxPollIntervalMs">The interval in milliseconds</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithMaxPollIntervalMs(int maxPollIntervalMs);

        /// <summary>
        /// Sets the number of threads that will be used to consume the messages
        /// </summary>
        /// <param name="workersCount">The number of workers</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithWorkersCount(int workersCount);

        /// <summary>
        /// Sets how many messages will be buffered for each worker
        /// </summary>
        /// <param name="size">The buffer size</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithBufferSize(int size);

        /// <summary>
        /// Sets the strategy to choose a worker when a message arrives
        /// </summary>
        /// <typeparam name="T">A class that implements the <see cref="IDistributionStrategy"/> interface</typeparam>
        /// <param name="factory">A factory to create the instance</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithWorkDistributionStrategy<T>(Factory<T> factory)
            where T : class, IDistributionStrategy;

        /// <summary>
        /// Sets the strategy to choose a worker when a message arrives
        /// </summary>
        /// <typeparam name="T">A class that implements the <see cref="IDistributionStrategy"/> interface</typeparam>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithWorkDistributionStrategy<T>()
            where T : class, IDistributionStrategy;

        /// <summary>
        /// Offsets will be stored after the execution of the handler and middlewares automatically, this is the default behaviour
        /// </summary>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithAutoStoreOffsets();

        /// <summary>
        /// The client should call the <see cref="IConsumerContext.StoreOffset()"/>
        /// </summary>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithManualStoreOffsets();

        /// <summary>
        /// Configures the consumer initial state.The default is <see cref="ConsumerInitialState.Running"/>
        /// </summary>
        /// <param name="state">The consumer state</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithInitialState(ConsumerInitialState state);

        /// <summary>
        /// Adds middlewares to the consumer. The middlewares will be executed in the registration order
        /// </summary>
        /// <param name="middlewares">A handler to register middlewares</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder AddMiddlewares(Action<IConsumerMiddlewareConfigurationBuilder> middlewares);

        /// <summary>
        /// Adds a handler for the Kafka consumer statistics
        /// </summary>
        /// <param name="statisticsHandler">A handler for the statistics</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithStatisticsHandler(Action<string> statisticsHandler);

        /// <summary>
        /// Sets the interval the statistics are emitted
        /// </summary>
        /// <param name="statisticsIntervalMs">The interval in milliseconds</param>
        /// <returns></returns>
        IConsumerConfigurationBuilder WithStatisticsIntervalMs(int statisticsIntervalMs);
    }
}
