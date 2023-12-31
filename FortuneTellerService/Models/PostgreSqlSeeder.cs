﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace FortuneTellerService.Models;

internal sealed class PostgreSqlSeeder
{
    public static async Task CreateSampleDataAsync(IServiceProvider serviceProvider)
    {
        await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();
        await using var fortuneDbContext = scope.ServiceProvider.GetRequiredService<FortuneContext>();

        await DropCreateTablesAsync(fortuneDbContext);
        await InsertSampleDataAsync(fortuneDbContext);
    }

    private static async Task DropCreateTablesAsync(DbContext dbContext)
    {
        bool wasCreated = await dbContext.Database.EnsureCreatedAsync();

        if (!wasCreated)
        {
            // The database already existed. Because apps usually don't have permission to drop the database,
            // we drop and recreate all the tables in the DbContext instead.
            var databaseCreator = (RelationalDatabaseCreator)dbContext.Database.GetService<IDatabaseCreator>();

            await DropTablesAsync(dbContext);
            await databaseCreator.CreateTablesAsync();
        }
    }

    private static async Task DropTablesAsync(DbContext dbContext)
    {
        IEnumerable<string> tableNames = dbContext.Model.GetEntityTypes().Select(type => type.GetSchemaQualifiedTableName()!);
        IEnumerable<string> dropStatements = tableNames.Select(tableName => "DROP TABLE IF EXISTS \"" + tableName + "\";");

        string sqlStatement = string.Join(Environment.NewLine, dropStatements);
        await dbContext.Database.ExecuteSqlRawAsync(sqlStatement);
    }

    private static async Task InsertSampleDataAsync(FortuneContext fortuneDbContext)
    {
        fortuneDbContext.Fortunes.AddRange(
            new() { Id = 1000, Text = "People are naturally attracted to you.", MessageFromBeyond = "I miss being alive" },
            new() { Id = 1001, Text = "You learn from your mistakes... You will learn a lot today." },
            new() { Id = 1002, Text = "If you have something good in your life, don't let it go!" },
            new() { Id = 1003, Text = "What ever you're goal is in life, embrace it visualize it, and for it will be yours.", MessageFromBeyond = "Looks like good fishing weather today"},
            new() { Id = 1004, Text = "Your shoes will make you happy today." },
            new() { Id = 1005, Text = "You cannot love life until you live the life you love.", MessageFromBeyond = "Carpe diem" },
            new() { Id = 1006, Text = "Be on the lookout for coming events; They cast their shadows beforehand." },
            new() { Id = 1007, Text = "Land is always on the mind of a flying bird.", MessageFromBeyond = "Tell my daughter I love her" },
            new() { Id = 1008, Text = "The man or woman you desire feels the same about you." },
            new() { Id = 1009, Text = "Meeting adversity well is the source of your strength." },
            new() { Id = 1010, Text = "A dream you have will come true.", MessageFromBeyond = "My life was too short" },
            new() { Id = 1011, Text = "Our deeds determine us, as much as we determine our deeds." },
            new() { Id = 1012, Text = "Never give up. You're not a failure if you don't give up.", MessageFromBeyond = "" },
            new() { Id = 1013, Text = "You will become great if you believe in yourself." },
            new() { Id = 1014, Text = "There is no greater pleasure than seeing your loved ones prosper.", MessageFromBeyond = "Boo!" },
            new() { Id = 1015, Text = "You will marry your lover." },
            new() { Id = 1016, Text = "A very attractive person has a message for you." },
            new() { Id = 1017, Text = "You already know the answer to the questions lingering inside your head." },
            new() { Id = 1018, Text = "It is now, and in this world, that we must live." },
            new() { Id = 1019, Text = "You must try, or hate yourself for not trying.", MessageFromBeyond = "We're waiting for you" },
            new() { Id = 1020, Text = "You can make your own happiness." },
            new() { Id = 1021, Text = "The greatest risk is not taking one." },
            new() { Id = 1022, Text = "The love of your life is stepping into your planet this summer.", MessageFromBeyond = "Have you seen my stapler?" },
            new() { Id = 1023, Text = "Love can last a lifetime, if you want it to." },
            new() { Id = 1024, Text = "Adversity is the parent of virtue." },
            new() { Id = 1025, Text = "Serious trouble will bypass you.", MessageFromBeyond = "You are in danger!" },
            new() { Id = 1026, Text = "A short stranger will soon enter your life with blessings to share." },
            new() { Id = 1027, Text = "Now is the time to try something new." },
            new() { Id = 1028, Text = "Wealth awaits you very soon." },
            new() { Id = 1029, Text = "If you feel you are right, stand firmly by your convictions." },
            new() { Id = 1030, Text = "If winter comes, can spring be far behind?", MessageFromBeyond = "Don't fear the reaper" },
            new() { Id = 1031, Text = "Keep your eye out for someone special." },
            new() { Id = 1032, Text = "You are very talented in many ways." },
            new() { Id = 1033, Text = "A stranger, is a friend you have not spoken to yet." },
            new() { Id = 1034, Text = "A new voyage will fill your life with untold memories.", MessageFromBeyond = "I finally discovered where my socks were going" },
            new() { Id = 1035, Text = "You will travel to many exotic places in your lifetime." },
            new() { Id = 1036, Text = "Your ability for accomplishment will follow with success." },
            new() { Id = 1037, Text = "Nothing astonishes men so much as common sense and plain dealing." },
            new() { Id = 1038, Text = "Its amazing how much good you can do if you don't care who gets the credit.", MessageFromBeyond = "I shouldn't have pushed the red button :(" },
            new() { Id = 1039, Text = "Everyone agrees. You are the best." },
            new() { Id = 1040, Text = "LIFE CONSIST NOT IN HOLDING GOOD CARDS, BUT IN PLAYING THOSE YOU HOLD WELL." },
            new() { Id = 1041, Text = "Jealousy doesn't open doors, it closes them!" },
            new() { Id = 1042, Text = "It''s better to be alone sometimes.", MessageFromBeyond = "" },
            new() { Id = 1043, Text = "When fear hurts you, conquer it and defeat it!" },
            new() { Id = 1044, Text = "Let the deeds speak." },
            new() { Id = 1045, Text = "You will be called in to fulfill a position of high honor and responsibility." },
            new() { Id = 1046, Text = "The man on the top of the mountain did not fall there." },
            new() { Id = 1047, Text = "You will conquer obstacles to achieve success." },
            new() { Id = 1048, Text = "Joys are often the shadows, cast by sorrows." },
            new() { Id = 1049, Text = "Fortune favors the brave." }
        );
        await fortuneDbContext.SaveChangesAsync();
    }
}