import {test, expect, firefox} from '@playwright/test';

test('has title1', async ({page}) => {
    await page.goto('https://playwright.dev/');

    // Expect a title "to contain" a substring.
    await expect(page).toHaveTitle(/Playwright/);
});

test('get started link', async ({page}) => {
    await page.goto('https://playwright.dev/');

    // Click the get started link.
    await page.getByRole('link', {name: 'Get started'}).click();

    // Expects the URL to contain intro.
    await expect(page).toHaveURL(/.*intro/);
});

test('YouTubeTest', async ({page}) => {

    await page.goto('https://www.youtube.com/');
    await page.getByText('Sign in').first().click();
    await page.locator('//input[@type="email"]').fill("");
    await page.locator("//button[@class='VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ VfPpkd-LgbsSe-OWXEXe-dgl2Hf nCP5yc AjY5Oe DuMIQc LQeN7 qIypjc TrZEUc lw1w4b']").click();
    await page.locator("//input[@type='password']").fill('');

    await page.locator("//button[@class='VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ VfPpkd-LgbsSe-OWXEXe-dgl2Hf nCP5yc AjY5Oe DuMIQc LQeN7 qIypjc TrZEUc lw1w4b']")
        .click();
    await page.waitForURL('https://www.youtube.com/');
    await page.goto("");
    await page.waitForTimeout(1000);
    await page.locator("//ytcp-button[@label='Create']").click();
    await page.getByText('Upload videos').first().click();
    
    const fileChooserPromise = page.waitForEvent('filechooser');
    await page.getByText('Select files').click();
    const fileChooser = await fileChooserPromise;
    await fileChooser.setFiles('C:\\Users\\skade\\Desktop\\Oleg\\TikTok\\Videos\\1.mp4',{noWaitAfter:false, timeout:4000});
    let locator = page.locator("//div[@aria-label='Add a title that describes your video (type @ to mention a channel)']")
    await locator.waitFor({state:"visible",timeout: 15000});
    /*  await page.setInputFiles('input[type="file"]', 'D:\\GitProjects\\AppDownloader\\Application\\Videos\\1.mp4');*/
    await page.locator("//div[@aria-label='Add a title that describes your video (type @ to mention a channel)']").fill('Funny Video');
    await page.locator("//div[@aria-label='Tell viewers about your video (type @ to mention a channel)']").fill('Video Decsription');
    await page.locator("//tp-yt-paper-radio-button[@name='VIDEO_MADE_FOR_KIDS_MFK']").scrollIntoViewIfNeeded({timeout:1000});
    await page.locator("//tp-yt-paper-radio-button[@name='VIDEO_MADE_FOR_KIDS_MFK']").click({timeout:1000});
    
/*    await page.getByText("Show more").click();
    await page.locator("//input[@placeholder='Add tag']").scrollIntoViewIfNeeded();
    await page.locator("//input[@placeholder='Add tag']").fill('tag1,tag2,tag3,tag4');*/
/*    await page.getByText('People & Blogs').click();*/
    await page.locator("//button[@id='step-badge-1']").click();
    await page.locator("//button[@id='step-badge-2']").click();
    await page.locator("//button[@id='step-badge-3']").click();
    await page.locator("//tp-yt-paper-radio-button[@name='PUBLIC']").click();
    await page.locator("//ytcp-button[@id='done-button']").click();
});