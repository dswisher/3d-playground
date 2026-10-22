# Devlog

A [Hugo](https://gohugo.io/) site published to GitHub Pages by
`.github/workflows/devlog.yml` whenever `devlog/` changes on `main`.

## Preview locally

```sh
cd devlog
hugo server -D        # -D includes drafts
```

Then open <http://localhost:1313/3d-playground/>.

## New post

```sh
cd devlog
hugo new content posts/2026-10-23-my-post-title/index.md
```

- Posts start as `draft = true`; set `draft = false` to publish.
- Put images next to `index.md` in the post folder and reference them as `![alt](screenshot.png)`.
- Posts dated in the future are not published.

## One-time GitHub setup

In the repo's **Settings → Pages**, set **Source** to **GitHub Actions**.
