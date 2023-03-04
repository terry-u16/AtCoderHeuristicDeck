using AtCoderHeuristicDeck.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddScoped<IContestHistoryClient, AtCoderContestHistoryClient>();
        s.AddScoped<DeckService>();
    })
    .Build();

host.Run();
