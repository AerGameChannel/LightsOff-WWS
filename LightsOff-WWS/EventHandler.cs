using System;
using WW_SYSTEM;
using WW_SYSTEM.EventHandlers;
using WW_SYSTEM.Events;

namespace LightsOff
{
    class EventHandler: IEventHandlerAdminQuery
    {
        public Plugin plugin;

        public EventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }
        public void OnAdminQuery(AdminQueryEvent ev)
        {
            string[] array = ev.Query.Split(new char[]
            {
                    ' '
            });
            if (ev.Query.ToLower().StartsWith("lo") || ev.Query.ToLower().StartsWith("lights"))
            {
                if (array.Length <= 1)
                {
                    ev.Output = "LightsOff#Usage: lo/lights <time in seconds>";
                    ev.Successful = true;
                    ev.Handled = true;
                    return;
                }
                if (string.IsNullOrEmpty(array[1]))
                {
                    ev.Output = "LightsOff#Usage: lo/lights <time in seconds>";
                    ev.Successful = true;
                    ev.Handled = true;
                    return;
                }
                if (array.Length > 1)
                {
                    try
                    {
                        if (!ev.Admin.IsPermitted(PlayerPermissions.FacilityManagement))
                        {
                            ev.Output = "LightsOff#Not enough permissions";
                            ev.Successful = false;
                            ev.Handled = true;
                            return;
                        }
                        if (array.Length > 1)
                        {
                            if (array[1].ToLower().Contains("help"))
                            {
                                ev.Output = "LightsOff#Usage: lo <time in seconds>";
                                ev.Successful = true;
                                ev.Handled = true;
                                return;
                            }
                            else
                            {
                                int lotime = int.Parse(array[1]);

                                Server.Round.EnableFlickering(lotime, false);

                                ev.Output = "LightsOff#Turned off the lights ";
                                ev.Successful = true;
                                ev.Handled = true;
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ev.Output = "LightsOff#Error: " + ex;
                        ev.Successful = false;
                        ev.Handled = true;
                        return;
                    }
                }
            }
        }
    }
}
