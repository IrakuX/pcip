<script setup lang="ts">
import type { ValidationErrors } from '~/types/ValidationErrors';

definePageMeta({
    layout: "login"
})

const form = ref({
    email: "",
});

const busy = ref(false);
const errors = ref<ValidationErrors>({});

const fnSubmitForm = () => {
    try {
        errors.value = {};
        busy.value = true;

        console.log(form.value);

        // // Callback function example to add 2FA feature.
        // await login<{ two_factor: boolean }>(form.value, {}, async (response) => {
        //   if (response.two_factor) {
        //     return await navigateTo(`/2fa`);
        //   }
        //   return await navigateTo(`/dashboard`);
        // });
    } catch (err) {
        busy.value = false;
        if (err instanceof FetchError && err.response?.status === 422) {
            errors.value = err.response._data.errors;
        }
    }
};


</script>
<template>
    <div class="customClassLogin">
        <MetaTags title="Login" description="Control de acceso" />
        <Card class="max-w-sm mx-auto">
            <CardHeader>
                <CardTitle class="text-2xl">
                    Recover your password
                </CardTitle>
                <CardDescription>
                    Please enter your email address to send an account recovery email.
                </CardDescription>
            </CardHeader>
            <CardContent>
                <form @submit.prevent="fnSubmitForm">
                    <div class="grid gap-4">
                        <div class="grid gap-2">
                            <Label for="email">Email</Label>
                            <Input v-model="form.email" type="email" placeholder="m@example.com" required />
                        </div>
                        <div class="grid gap-2">
                            <div class="flex items-center">
                                <NuxtLink to="/auth/login" class="inline-block ml-auto text-sm underline">
                                    Return to login
                                </NuxtLink>
                            </div>

                        </div>
                        <Button type="submit" class="w-full">
                            Send email
                        </Button>
                    </div>
                </form>
            </CardContent>
        </Card>
    </div>
</template>
