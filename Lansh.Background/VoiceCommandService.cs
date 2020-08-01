using Lansh.Common.Model;
using Lansh.Common.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Resources.Core;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Lansh.Background
{
    public sealed class VoiceCommandService : IBackgroundTask
    {
        VoiceCommandServiceConnection voiceCommandserviceConnection;
        private ResultService _resultService = new ResultService();

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            taskInstance.Canceled += OnTaskCanceled;
            AppServiceTriggerDetails triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            var resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Resources");
            var cortanaContext = ResourceContext.GetForViewIndependentUse();
            if (triggerDetails != null && triggerDetails.Name == "LanshVoiceCommandService")
            {
                try
                {
                    voiceCommandserviceConnection = VoiceCommandServiceConnection.FromAppServiceTriggerDetails(triggerDetails);
                    voiceCommandserviceConnection.VoiceCommandCompleted += OnVoiceCommandCompleted;
                    VoiceCommand voiceCommand = await voiceCommandserviceConnection.GetVoiceCommandAsync();
                    switch (voiceCommand.CommandName)
                    {
                        case "recommendVideo":
                            string number = voiceCommand.Properties["number"].FirstOrDefault();
                            System.Diagnostics.Debug.WriteLine("number: " + number + "------------");
                            foreach (var i in voiceCommand.SpeechRecognitionResult.SemanticInterpretation.Properties)
                            {
                                System.Diagnostics.Debug.WriteLine("i: " + i + "------------");
                            }

                            //string key = voiceCommand.Properties["*"][0];
                            RecommendVideo(number, "搞笑");
                            break;
                        case "oneOne":
                            await OneOne();
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
            deferral.Complete();
        }

        private async void RecommendVideo(string number, string key)
        {
            var data = await IBaseData.GetData();
            VoiceCommandUserMessage voiceCommandUserMessage = new VoiceCommandUserMessage();
            voiceCommandUserMessage.DisplayMessage = key + "视频";
            voiceCommandUserMessage.SpokenMessage = "为你找到了如下视频";

            VoiceCommandUserMessage choice = new VoiceCommandUserMessage();
            choice.DisplayMessage = "选择视频";
            choice.SpokenMessage = "客官,来选一个吧";

            VoiceCommandResponse response = VoiceCommandResponse.CreateResponseForPrompt(voiceCommandUserMessage, choice, data);
            VoiceCommandDisambiguationResult selectVideo = await voiceCommandserviceConnection.RequestDisambiguationAsync(response);
            VoiceCommandContentTile selecteItem = selectVideo.SelectedItem;

            voiceCommandUserMessage.DisplayMessage = voiceCommandUserMessage.SpokenMessage = "好了，就它了";
            response = VoiceCommandResponse.CreateResponse(voiceCommandUserMessage);
            await voiceCommandserviceConnection.ReportSuccessAsync(response);
        }

        private async Task OneOne()
        {
            List<VoiceCommandContentTile> contentTiles = new List<VoiceCommandContentTile>();
            List<One> oneList = await _resultService.GetOneList();
            foreach (One one in oneList)
            {
                string title = one.Title;
                string forward = one.Forward;
                if (title.Length > 50 || forward.Length > 100)
                    break;
                VoiceCommandContentTile tile = new VoiceCommandContentTile();
                tile.ContentTileType = VoiceCommandContentTileType.TitleWithText;
                //tile.Image = one
                tile.Title = title;
                tile.TextLine1 = forward;
                contentTiles.Add(tile); //杂货店来点正能量 一二三四五六七八九十一
            }
            var data = await IBaseData.GetData();
            VoiceCommandUserMessage voiceCommandUserMessage = new VoiceCommandUserMessage();
            voiceCommandUserMessage.DisplayMessage = "热腾腾的鸡汤";
            voiceCommandUserMessage.SpokenMessage = "你就不会找个大点的碗嘛";
            VoiceCommandResponse response = VoiceCommandResponse.CreateResponse(voiceCommandUserMessage, contentTiles);
            await voiceCommandserviceConnection.ReportSuccessAsync(response);
        }

        private void OnVoiceCommandCompleted(VoiceCommandServiceConnection sender, VoiceCommandCompletedEventArgs args)
        {
            //throw new NotImplementedException();
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            //throw new NotImplementedException();
        }
    }

    internal class IBaseData
    {
        public async static Task<IEnumerable<VoiceCommandContentTile>> GetData()
        {
            IList<VoiceCommandContentTile> contentTitles = new List<VoiceCommandContentTile>();

            var filedatas = await GetFiles();

            //VoiceCommandContentTile imgTest = new VoiceCommandContentTile();

            //imgTest.AppLaunchArgument = "what?";
            //imgTest.ContentTileType = VoiceCommandContentTileType.TitleWith280x140IconAndText;
            //imgTest.Title = "Title";
            //imgTest.TextLine1 = "TextLine1";
            //imgTest.TextLine2 = "TextLine2";
            ////StorageFile sf = await StorageFile.GetFileFromApplicationUriAsync(new Uri("http://i0.hdslb.com/bfs/archive/e524aca5865b98089eaf4fd3004772dd343f47d8.jpg@.webp"));
            //StorageFile sf = await StorageFile.GetFileFromApplicationUriAsync(new Uri("4.png"));
            //imgTest.Image = sf;
            //contentTitles.Add(imgTest);
            //RenderTargetBitmap bitmap = new RenderTargetBitmap();

            //Image image = new Image();
            //image.GetAsCastingSource().PreferredSourceUri = new Uri(""); 

            //imgTest.Image

            for (uint n = 0; n < filedatas.Length; n++)
            {
                if (contentTitles.Count >= VoiceCommandResponse.MaxSupportedVoiceCommandContentTiles)
                {
                    break;
                }
                VoiceCommandContentTile tile = new VoiceCommandContentTile();
                tile.ContentTileType = VoiceCommandContentTileType.TitleWith68x68IconAndText;
                tile.Image = filedatas[n].Item1;
                tile.Title = filedatas[n].Item2;
                tile.TextLine1 = filedatas[n].Item3;
                contentTitles.Add(tile);
            }
            return contentTitles.ToArray();
        }

        public async static Task<Tuple<StorageFile, string, string>[]> GetFiles()
        {
            Tuple<StorageFile, string, string>[] tuples = new Tuple<StorageFile, string, string>[5];
            for (int i = 1; i < 6; i++)
            {

                StorageFile img = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/StoreLogo.png")); //ms-appx:///Assets/StoreLogo.png
                Tuple<StorageFile, string, string> tuple = new Tuple<StorageFile, string, string>(img, i + "个个个个个个", i + "lalala");
                tuples[i - 1] = tuple;
            }
            return tuples;
        }
    }

}