using AngleSharp.Dom;
using AngleSharp;
using AngleSharp.Svg.DataTypes;
using AngleSharp.Css;
using AngleSharp.Svg.Values;

IConfiguration config = Configuration.Default.WithDefaultLoader().WithRenderDevice(new DefaultRenderDevice()
{
    DeviceWidth = 1280,
    DeviceHeight = 720
});

string address = "https://en.wikipedia.org/wiki/List_of_The_Big_Bang_Theory_episodes";
IBrowsingContext context = BrowsingContext.New(config);
IDocument document = await context.OpenAsync(address);

IElement mum = document.CreateElement("yourMum");
mum.SetAttribute("size", "69");

var computeContext = new SvgComputeContext(context);

var mumSize = new SvgNumber(mum, "size", computeContext);
Console.WriteLine(mumSize.Value);
mum.SetAttribute("size", "23.5");
Console.WriteLine(mumSize.Value);
mum.SetAttribute("size", "calc(25px - 8px)");
Console.WriteLine(mumSize.Value);

mum.SetAttribute("fuckability", "-352");

mumSize.Value = 420.723f;
Console.WriteLine(mum.GetAttribute("size"));

mum.SetAttribute("size", "yes");