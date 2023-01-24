// See https://aka.ms/new-console-template for more information
using Speckle.Core.Credentials;
using Speckle.Core.Transports;
using Speckle.Core.Api;
using Speckle.Core.Models;
Console.WriteLine("Hello, Speckle!");


var line1 = new Objects.Geometry.Line(new Objects.Geometry.Point(0, 0, 0), new Objects.Geometry.Point(100, -200, 50));
var line2 = new Objects.Geometry.Line(new Objects.Geometry.Point(0, 0, 0), new Objects.Geometry.Point(500, 100, 600));

var commitData = new Base();

commitData["data"] = new List<Base> { line1, line2 };

var account = new Account();
account.token = "<replace with your token>";
account.serverInfo = new ServerInfo
{
    url = "https://speckle.xyz/"
};
var streamId = "<replace with your stream id>";
var branchName = "main";
var client = new Client(account);
var transport = new ServerTransport(account, streamId);

var objectId = await Operations.Send(
    commitData,
    new List<ITransport> { transport },
    disposeTransports: true);

var commitId = await client.CommitCreate(
    new CommitCreateInput
    {
        streamId = streamId,
        branchName = branchName,
        objectId = objectId,
        message = "Hello"
    });
Console.WriteLine($"https://speckle.xyz/streams/{streamId}");